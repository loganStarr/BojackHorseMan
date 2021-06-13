#include <Arduino.h>
#define X A4
#define Y A5
void setup() {
  Serial.begin(115200);
}

void loop() {
  // put your main code here, to run repeatedly:
  Serial.print("R");
  Serial.print(analogRead(X));
  Serial.print("XR");
  Serial.print(analogRead(Y));
  Serial.println("Y"); 
}