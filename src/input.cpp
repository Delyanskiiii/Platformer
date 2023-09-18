#include "input.h"

Input::Input() {
    input.Set(0, 0);
    prevInput.Set(0, 0);
}

/**
 * Given four booleans that represent if the buttons are being
 * held down, it calculates the direction intended by the player.
 * 
 * @param Left Intended key for moving horizontally to the left.
 * @param Right Intended key for moving horizontally to the right.
 * @param Up Intended key for moving vertically up.
 * @param Down Intended key for moving vertically down.
 * 
 * @return Pair containing the input as value1 represents horizontal
 * direction(Left = -1, 0, Right = 1) and value2 represents vertical(Down = -1, 0, Up = 1).
 */
Pair Input::GetInput(bool Left, bool Right, bool Up, bool Down) {
    if (Left && Right)
    {
        // Checks for prevInput so it doesn't switch directions every frame when both keys are pressed. That's used because pressing left while right is being held the character will swap directions.
        if (prevInput.value1 == 1)
        {
            if (input.value1 == 1)
            {
                input.value1 = -1;
            }
            else
            {
                input.value1 = 1;
            }
            prevInput.value1 = 0;
        }
    }
    else if (Left && !Right)
    {
        input.value1 = -1;
        prevInput.value1 = 1;
    }
    else if (!Left && Right)
    {
        input.value1 = 1;
        prevInput.value1 = 1;
    }
    else
    {
        input.value1 = 0;
        prevInput.value1 = 1;
    }

    if (Up && Down)
    {
        if (prevInput.value2 == 1)
        {
            if (input.value2 == 1)
            {
                input.value2 = -1;
            }
            else
            {
                input.value2 = 1;
            }
            prevInput.value2 = 0;
        }
    }
    else if (Up && !Down)
    {
        input.value2 = -1;
        prevInput.value2 = 1;
    }
    else if (!Up && Down)
    {
        input.value2 = 1;
        prevInput.value2 = 1;
    }
    else
    {
        input.value2 = 0;
        prevInput.value2 = 1;
    }

    return input;
}