// BMP 085
#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_BMP085_U.h>
// DHT 22
#include <DHT.h>
// TroykaLight
#include <TroykaLight.h>
// SERVER
#include <ESP8266WiFi.h>
#include <ESPAsyncTCP.h>
#include <ESPAsyncWebServer.h>
#include <ArduinoJson.h>
// PINS
#define PIN_DHT22 13
#define LIGHT_PIN A0
// ERROR/SUCCESS
#define SUCCESS 0
#define ERROR -1
// coeffs
#define LIGHT_RESISTOR_COEFF 15 // calibration coeffitient

// wifi credentials
const char* ssid = "YOUR_SSID";
const char* password = "YOUR_PASSWORD";

// you can avoid this part and use DHCP
IPAddress subnet(255, 255, 255, 0);
IPAddress gateway(192, 168, 50, 1);      
IPAddress local_IP(192, 168, 50, 111); // static: 192.168.50.111

// sensors variables  
Adafruit_BMP085_Unified bmp = Adafruit_BMP085_Unified(10085); // uses BMP180
DHT dht22(PIN_DHT22, DHT22); // uses DHT22
TroykaLight sensorLight(LIGHT_PIN); // uses TroykaLight with GL5528

// server variable
AsyncWebServer server(80);

// values
float temperature = 0, pressure = 0, humidity = 0, light = 0;

// scanner functions (pressure, temperature, humidity, light)
int scanPressure()
{
  sensors_event_t event;
  bmp.getEvent(&event);
  if (event.pressure)
  {
    pressure = event.pressure * 100; // Pa
    return 0;
  }
  else
  {
    return ERROR;
  }
}

int scanTemperatureAndHumidity()
{
  float h = dht22.readHumidity(); // relative humidity (percents)
  float t = dht22.readTemperature(); // celsius
  
  if (isnan(h) || isnan(t)) 
  {
    Serial.println("NAN");
    return ERROR;
  }
  temperature = t;
  humidity = h;
  return SUCCESS;
}

int scanLight()
{
  sensorLight.read();
  float l = LIGHT_RESISTOR_COEFF * sensorLight.getLightLux(); // Lux
  if (isnan(l) || l == 0)
  {
    return ERROR;
  }
  light = l;
  return SUCCESS;
}

// returns json with cached values
void setupServer(void)
{
  WiFi.config(local_IP, gateway, subnet);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(1500);
    Serial.println("Подключение...");
  }
  Serial.println(WiFi.localIP());
  server.on("/", HTTP_GET, [](AsyncWebServerRequest *request){
    AsyncResponseStream *response = request->beginResponseStream("application/json; charset=utf-8");
    JsonDocument root;
    root["temperature"] = temperature;
    root["light"] = light;
    root["pressure"] = pressure;
    root["humidity"] = humidity;
    serializeJson(root, *response);
    request->send(response);
  });

  server.begin();
}

void setup(void) 
{
  Serial.begin(115200);
  
  if(!bmp.begin())
  {
    Serial.print("Не удалось подключить BMP085. Стоит проверить соединение проводов");
    while(1);
  }
  dht22.begin();
  setupServer();
}

// cache all values from sensors every 5 seconds
void loop(void) 
{
    int result = 
      scanTemperatureAndHumidity() | 
      scanPressure() |
      scanLight();

    if (result == ERROR)
    {
      Serial.println("Ошибка при чтении значений");
    }
    // DHT needs some time to rest
    delay(5000);
}