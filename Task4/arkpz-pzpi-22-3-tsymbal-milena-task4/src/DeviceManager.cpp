#include <Arduino.h>
#include <ArduinoJson.h>
#include <HTTPClient.h>
#include "DeviceManager.h"
#include "DeviceConfig.h"
#include "models/User.h"
#include "models/DeviceLocation.h"

// API endpoint for interacting with the device
const char *API_DEVICE_PATH = "https://rnohu-193-138-245-54.a.free.pinggy.link/api/Device";

// Method to update the device status (active or inactive)
void DeviceManager::updateDeviceStatus(String deviceId, DeviceStatus status)
{
    HTTPClient http;

    String deviceUrl = String(API_DEVICE_PATH) + "/" + deviceId + "/status";
    Serial.print("Updating device status via: ");
    Serial.println(deviceUrl);

    http.begin(deviceUrl);
    http.addHeader("Content-Type", "application/json");

    String requestBody = (status == DeviceStatus::Active) ? "0" : "1";

    Serial.print("Request Body: ");
    Serial.println(requestBody);

    int statusCode = http.PATCH(requestBody);

    if (statusCode > 0)
    {
        Serial.print("HTTP Response Code: ");
        Serial.println(statusCode);

        String responseBody = http.getString();
        Serial.print("Response: ");
        Serial.println(responseBody);
    }
    else
    {
        Serial.print("HTTP Request failed, error: ");
        Serial.println(statusCode);
    }

    http.end();
}

// Method to get the device's location
DeviceLocation DeviceManager::getDeviceLocation(String deviceId)
{
    HTTPClient http;

    String deviceUrl = String(API_DEVICE_PATH) + "/" + deviceId + "/location";
    Serial.print("Getting device location via: ");
    Serial.println(deviceUrl);

    http.begin(deviceUrl);
    http.addHeader("Content-Type", "application/json");

    int statusCode = http.GET();

    DeviceLocation location;

    if (statusCode > 0)
    {
        Serial.print("HTTP Response Code: ");
        Serial.println(statusCode);

        if (statusCode == HTTP_CODE_OK)
        {
            String response = http.getString();
            Serial.print("Response: ");
            Serial.println(response);

            StaticJsonDocument<256> responseJson;
            DeserializationError error = deserializeJson(responseJson, response);

            if (!error)
            {
                location.X = responseJson.containsKey("x") ? responseJson["x"].as<float>() : 0.0f;
                location.Y = responseJson.containsKey("y") ? responseJson["y"].as<float>() : 0.0f;
                location.Z = responseJson.containsKey("z") ? responseJson["z"].as<float>() : 0.0f;

                location.Height = responseJson.containsKey("height") ? responseJson["height"].as<float>() : 0.0f;
                location.Width = responseJson.containsKey("width") ? responseJson["width"].as<float>() : 0.0f;
                location.Length = responseJson.containsKey("length") ? responseJson["length"].as<float>() : 0.0f;

                Serial.println("Device location retrieved successfully.");
            }
            else
            {
                Serial.print("JSON Parsing Error: ");
                Serial.println(error.c_str());
            }
        }
        else
        {
            Serial.println("Failed to retrieve device location.");
        }
    }
    else
    {
        Serial.print("HTTP Request failed, error: ");
        Serial.println(http.errorToString(statusCode).c_str());
    }

    http.end();
    return location;
}

// Method to get users nearby a specific device
UsersInZone DeviceManager::getUsersNearby(String deviceId)
{
    HTTPClient http;

    String deviceUrl = String(API_DEVICE_PATH) + "/" + deviceId + "/usersInZone";
    Serial.print("Getting users in device zone via: ");
    Serial.println(deviceUrl);

    http.begin(deviceUrl);
    http.addHeader("Content-Type", "application/json");

    int statusCode = http.GET();

    if (statusCode > 0)
    {
        Serial.print("HTTP Response Code: ");
        Serial.println(statusCode);

        if (statusCode == HTTP_CODE_OK)
        {
            String response = http.getString();
            Serial.print("Response: ");
            Serial.println(response);

            StaticJsonDocument<512> responseJson;
            DeserializationError error = deserializeJson(responseJson, response);
            if (!error)
            {
                UsersInZone usersInZone;

                JsonArray usersArray = responseJson["users"].as<JsonArray>();
                for (JsonObject user : usersArray)
                {
                    User userObj;
                    userObj.UserId = user["userId"].as<String>();
                    userObj.X = user["x"].as<float>();
                    userObj.Y = user["y"].as<float>();
                    userObj.Z = user["z"].as<float>();

                    usersInZone.Users.push_back(userObj);
                }
                return usersInZone;
            }
            else
            {
                Serial.print("JSON Parsing Error: ");
                Serial.println(error.c_str());
            }
        }
    }
    else
    {
        Serial.print("HTTP Request failed, error: ");
        Serial.println(http.errorToString(statusCode).c_str());
    }

    http.end();

    return UsersInZone();
}
