#include "WifiManager.h"
#include "DeviceManager.h"
#include "DeviceConfig.h"
#include "MovementManager.h"
#include "models/DeviceLocation.h"
#include "models/UsersInZone.h"

const char *DEVICE_ID = "1A396E7F-8027-4273-9C7B-19147C412E03";
UsersInZone users;
DeviceLocation deviceLocation;

void setup()
{
  Serial.begin(115200);

  connectToWiFi();

  DeviceManager::updateDeviceStatus(DEVICE_ID, DeviceStatus::Active);

  deviceLocation = DeviceManager::getDeviceLocation(DEVICE_ID);
}

void loop()
{
  users = DeviceManager::getUsersNearby(DEVICE_ID);

  if (users.Users.size() > 0)
  {
    Serial.println("Checking if users are in device location...");

    MovementManager::processUsersAndCreateMovements(users, deviceLocation, DEVICE_ID);

    delay(2000);
  }
  else
  {
    Serial.println("Waiting for users...");
    
    delay(3000);
  }
}
