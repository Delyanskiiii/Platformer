#pragma once
#include <raylib.h>

class Input
{
    public:
        Input() : prevInput{0}, input{0} { }
        Vector2 GetInput();

    private:
        Vector2 prevInput;
        Vector2 input;
};