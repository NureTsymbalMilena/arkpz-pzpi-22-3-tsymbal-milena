#ifndef MOVEMENT_MANAGER_H
#define MOVEMENT_MANAGER_H
#include "models/User.h"
#include "models/DeviceLocation.h"
#include "models/UsersInZone.h"

class MovementManager {
public:
    static void createNewMovement(String deviceId, String userId);
    static void processUsersAndCreateMovements(const UsersInZone &users, const DeviceLocation &deviceLocation, const String &deviceId);
};

#endif
