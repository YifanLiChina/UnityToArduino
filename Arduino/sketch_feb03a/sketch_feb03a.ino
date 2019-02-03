//int motorPin = 6;
int motorSpeed = 0;
//String message;
char buffer[13];
int received = 0;

void setup() {

  pinMode(6, OUTPUT);
  pinMode(5, OUTPUT);
  Serial.begin(9600);
  while(!Serial);
//  Serial.println("Speed 0 to 255");
}

void loop() {  
//first step: try to run it
//  digitalWrite(motorPin, HIGH);
    
//second step: speed control
//  if (Serial.available()){
//    int speed = Serial.parseInt();
//    if (speed >= 0 && speed <= 255){
//      analogWrite(motorPin, speed);
//    }
//  }

//third step: receive data from Unity, println() works, print() will crash Unity
  Serial.flush();
  if (Serial.available() > 0){
//    message = Serial.read();
//    Serial.println(message);
    buffer[received++] = Serial.read();
    buffer[received] = '\0';
    if (received >= (sizeof(buffer) - 1)){
      Serial.println(buffer);
      char* motorPin;
      char* motorFre;
      char* buf;
      char* ptr;
//      char* motorPow;
      buf = strtok_r(buffer, ",", &ptr);
      motorPin = buf;
      buf = strtok_r(NULL, ",", &ptr);
      motorFre = buf;
//      motorPow = strtok(NULL, ',');
      int iMotorPin = atoi(motorPin);
      int iMotorFre = atoi(motorFre);
      Serial.println(iMotorPin);
      Serial.println(iMotorFre);
      analogWrite(iMotorPin, iMotorFre);
      buf =strtok_r(NULL, ",", &ptr);
      motorPin = buf;
      buf = strtok_r(NULL, ",", &ptr);
      motorFre = buf;
      iMotorPin = atoi(motorPin);
      iMotorFre = atoi(motorFre);
      Serial.println(iMotorPin);
      Serial.println(iMotorFre);
      analogWrite(iMotorPin, iMotorFre);
      received = 0;
//      delay(3000);
      delay(1000);
    }
  }
}
