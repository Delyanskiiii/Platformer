#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;
layout(origin_upper_left, pixel_center_integer) in vec4 gl_FragCoord;

uniform sampler2D Texture;
uniform vec2 lightProps;
uniform vec2 lightSource;
uniform vec2 resolution;

out vec4 finalColor;

float Distance(vec2 var1, vec2 var2) {
    if (abs(var1.x - var2.x) > abs(var1.y - var2.y)) {
        return abs(var1.x - var2.x);
    } else {
        return abs(var1.y - var2.y);
    }  
}

float Circle(vec2 var1, vec2 var2, bool horizontal) {
    if (abs(var1.x - var2.x) <= 25 && abs(var1.y - var2.y) <= 25) {
        if (abs(var1.x - var2.x) < abs(var1.y - var2.y)) {
            return abs(var1.x - var2.x);
        } else {
            return abs(var1.y - var2.y);
        }  
    } else{
        if (abs(var1.x - var2.x) > abs(var1.y - var2.y)) {
            return abs(var1.x - var2.x);
        } else {
            return abs(var1.y - var2.y);
        } 
    }
    //return floor(sqrt((pow(var1.x - var2.x, 2) + pow(var1.y - var2.y, 2))) + 0.5);
}

float Shadow(float circle, float alpha) {
    // if (alpha < 0.01) {
    //     //return 100;
    // }
    if (alpha >= 0.25) {
        return 25;
    } else if (circle > alpha * 100) {
        return 0;
    } else {
        return (alpha * 100 - circle);
    }
}

float ShadowStrength(float rootPixelAlpha, vec2 lightPixelCoord, vec2 rootPixelCoord) {
    float strength = 0;
    float var = 0;
    float a = (rootPixelCoord.y - lightPixelCoord.y) / (rootPixelCoord.x - lightPixelCoord.x);
    float b = rootPixelCoord.y - a * rootPixelCoord.x;
    bool vertical = false;

    if (rootPixelCoord.x == lightPixelCoord.x) {
        vertical = true;
    }

    vec2 pos;
    vec2 variance = vec2(-1, -1);
    bool horizontal = false;

    if (abs(lightPixelCoord.x - rootPixelCoord.x) > abs(lightPixelCoord.y - rootPixelCoord.y)) {
        horizontal = true;
        if (lightPixelCoord.x > rootPixelCoord.x) {
            pos = vec2(rootPixelCoord.x, lightPixelCoord.x);
        } else {
            pos = vec2(lightPixelCoord.x, rootPixelCoord.x);
        }
    } else {
        if (lightPixelCoord.y > rootPixelCoord.y) {
            pos = vec2(rootPixelCoord.y, lightPixelCoord.y);
        } else {
            pos = vec2(lightPixelCoord.y, rootPixelCoord.y);
        }
    }
    
    if (lightPixelCoord.x > rootPixelCoord.x) {
        variance = vec2(1, variance.y);
    }

    if (lightPixelCoord.y > rootPixelCoord.y) {
        variance = vec2(variance.x, 1);
    }

    ivec2 currentCoord;
    for (float i = pos.x; i <= pos.y; i += 1) {
        if (horizontal) {
            currentCoord = ivec2(i, floor(i * a + b + 0.5));
        } else {
            if (vertical) {
                currentCoord = ivec2(rootPixelCoord.x, i);
            } else {
                currentCoord = ivec2(floor((i - b) / a + 0.5), i);
            }
        }

        vec4 currentColor = texelFetch(Texture, currentCoord, 0);
        vec4 horColor = texelFetch(Texture, ivec2(currentCoord.x + variance.x, currentCoord.y), 0);
        vec4 verColor = texelFetch(Texture, ivec2(currentCoord.x, currentCoord.y + variance.y), 0);

        if (currentColor.a > rootPixelAlpha) {
            var = Shadow(Circle(currentCoord, rootPixelCoord, true), currentColor.a - rootPixelAlpha);
            if (var > strength) {
                strength = var;
            }
        } else if (horColor.a > rootPixelAlpha && verColor.a > rootPixelAlpha) {
            var = Shadow(Circle(currentCoord, rootPixelCoord, true), (verColor.a + horColor.a) / 2 - rootPixelAlpha);
            if (var > strength) {
                strength = var;
            }
        }
    }

    return strength;
}

void main() {
    vec4 texColor = texture(Texture, fragTexCoord);

    //Skip pixel if it's not on the level layer
    // if (texColor.a == 0) {
    //     discard;
    // }
    
    float distance = Distance(lightSource.xy, gl_FragCoord.xy);
    // float strength = CalculateShadowStrength(texColor.a, lightSource, gl_FragCoord);
    float strength = ShadowStrength(texColor.a, lightSource.xy, gl_FragCoord.xy);

    if (strength > 0) {
        finalColor = vec4(texColor.xyz - strength / 150, 1);
    } else {
        finalColor = vec4(texColor.xyz + (1 - distance / 320) / 5, 1);
    }
}