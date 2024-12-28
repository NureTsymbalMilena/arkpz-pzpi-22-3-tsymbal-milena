#ifndef DEVICE_MANAGER_H
#define DEVICE_MANAGER_H

#include <WString.h>
#include "models/DeviceLocation.h"
#include "DeviceConfig.h"
#include "models/UsersInZone.h"

class DeviceManager {
public:
    static void updateDeviceStatus(String deviceId,  DeviceStatus status);
    static DeviceLocation getDeviceLocation(String deviceId);
    static UsersInZone getUsersNearby(String deviceId);
};
#endif
