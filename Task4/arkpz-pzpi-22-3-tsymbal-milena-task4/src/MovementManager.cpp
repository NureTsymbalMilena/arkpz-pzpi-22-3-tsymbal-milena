#include <Arduino.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>
#include "MovementManager.h"
#include "models/UsersInZone.h"

// API endpoint for handling movement-related requests
const char *API_MOVEMENT_PATH = "https://rnohu-193-138-245-54.a.free.pinggy.link/api/Movement";

// Method to process users within a specific device's zone and create movements accordingly
void MovementManager::processUsersAndCreateMovements(const UsersInZone &users, const DeviceLocation &deviceLocation, const String &deviceId)
{
    float halfWidth = deviceLocation.Width / 2;
    float halfHeight = deviceLocation.Height / 2;
    float halfLength = deviceLocation.Length / 2;

    for (const auto &user : users.Users)
    {
        bool isInRoom =
            (user.X >= (deviceLocation.X - halfWidth) && user.X <= (deviceLocation.X + halfWidth)) &&
            (user.Y >= (deviceLocation.Y - halfHeight) && user.Y <= (deviceLocation.Y + halfHeight)) &&
            (user.Z >= (deviceLocation.Z - halfLength) && user.Z <= (deviceLocation.Z + halfLength));

        if (isInRoom)
        {
            Serial.print("User ");
            Serial.print(user.UserId);
            Serial.println(" is within the device's zone. Creating movement...");
            MovementManager::createNewMovement(deviceId, user.UserId);
        }
        else
        {
            Serial.print("User ");
            Serial.print(user.UserId);
            Serial.println(" is outside the device's room.");
        }
    }
}

// Method to create a new movement for a user in the device's zone
void MovementManager::createNewMovement(String deviceId, String userId)
{
    HTTPClient http;

    Serial.print("Creating new movement via: ");
    Serial.println(API_MOVEMENT_PATH);

    http.begin(API_MOVEMENT_PATH);
    http.addHeader("Content-Type", "application/json");

    StaticJsonDocument<200> jsonDoc;
    jsonDoc["deviceId"] = deviceId;
    jsonDoc["userId"] = userId;

    String requestBody;
    serializeJson(jsonDoc, requestBody);

    Serial.print("Request Body: ");
    Serial.println(requestBody);

    int statusCode = http.POST(requestBody);

    if (statusCode > 0)
    {
        Serial.print("HTTP Response Code: ");
        Serial.println(statusCode);
    }
    else
    {
        Serial.print("HTTP Request failed, error: ");
        Serial.println(statusCode);
    }

    http.end();
}
