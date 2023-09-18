#include "input.h"

Input::Input() {
    input.Set(0, 0);
    prevInput.Set(0, 0);
}

Pair Input::GetInput(bool Left, bool Right, bool Up, bool Down) {
    if (Left && Right)
    {
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