#include "InternetButton/InternetButton.h"

InternetButton b = InternetButton();

void setup() {
    b.begin();
}

void loop() {
    if(b.buttonOn(2)){
        Particle.publish("button-press","East");
    } else if (b.buttonOn(3)){
        Particle.publish("button-press","South");
    } else if (b.buttonOn(4)){
        Particle.publish("button-press","West");
    } else if (b.buttonOn(1)){
        Particle.publish("button-press","North");
    }
    delay(120);
}