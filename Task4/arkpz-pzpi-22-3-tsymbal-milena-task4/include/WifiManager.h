#ifndef WIFI_MANAGER_H
#define WIFI_MANAGER_H

#include <WiFi.h>
#include <WebServer.h>

void connectToWiFi();
void handleRoot();
void handleSetWiFi();


#endif