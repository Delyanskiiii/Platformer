#include "input.h"
#include <raylib.h>

/**
 * Given four booleans that represent if the buttons are being
 * held down, it calculates the direction intended by the player.
 * 
 * @param Left Intended key for moving horizontally to the left.
 * @param Right Intended key for moving horizontally to the right.
 * @param Up Intended key for moving vertically up.
 * @param Down Intended key for moving vertically down.
 * 
 * @return Pair containing the input as x represents horizontal
 * direction(Left = -1, 0, Right = 1) and y represents vertical(Down = -1, 0, Up = 1).
 */
Vector2 Input::GetInput() {
    bool Left = IsKeyDown(KEY_LEFT);
    bool Right = IsKeyDown(KEY_RIGHT);
    bool Up = IsKeyDown(KEY_SPACE);
    bool Down = IsKeyDown(KEY_DOWN);

    if (Left && Right)
    {
        // Checks for prevInput so it doesn't switch directions every frame when both keys are pressed. That's used because pressing left while right is being held the character will swap directions.
        if (prevInput.x == 1)
        {
            if (input.x == 1)
            {
                input.x = -1;
            }
            else
            {
                input.x = 1;
            }
            prevInput.x = 0;
        }
    }
    else if (Left && !Right)
    {
        input.x = -1;
        prevInput.x = 1;
    }
    else if (!Left && Right)
    {
        input.x = 1;
        prevInput.x = 1;
    }
    else
    {
        input.x = 0;
        prevInput.x = 1;
    }

    if (Up && Down)
    {
        if (prevInput.y == 1)
        {
            if (input.y == 1)
            {
                input.y = -1;
            }
            else
            {
                input.y = 1;
            }
            prevInput.y = 0;
        }
    }
    else if (Up && !Down)
    {
        input.y = -1;
        prevInput.y = 1;
    }
    else if (!Up && Down)
    {
        input.y = 1;
        prevInput.y = 1;
    }
    else
    {
        input.y = 0;
        prevInput.y = 1;
    }

    return input;
}