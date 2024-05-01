#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D ourTexture;
uniform vec2 lightProps;
uniform vec2 lightSource;
uniform vec2 resolution;

out vec4 finalColor;

bool CalculateColorVariance(float alpha, vec2 lightPixelCoord, vec2 currentPixelCoord) {
    if (abs(lightPixelCoord.x - currentPixelCoord.x) <= 1 && abs(lightPixelCoord.y - currentPixelCoord.y) <= 1) {
        return false;
    } else if (abs(lightPixelCoord.x - currentPixelCoord.x) > abs(lightPixelCoord.y - currentPixelCoord.y)) {
        float a = (currentPixelCoord.y - lightPixelCoord.y) / (currentPixelCoord.x - lightPixelCoord.x);
        float b = currentPixelCoord.y - a * currentPixelCoord.x;

        if (lightPixelCoord.x > currentPixelCoord.x) {
            for (float i = currentPixelCoord.x + 1; i < lightPixelCoord.x; i += 1) {
                vec4 currentColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b) / resolution.y));
                vec4 leftColor = texture(ourTexture, vec2((i - 1) / resolution.x, (i * a + b) / resolution.y));
                vec4 upColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b + 1) / resolution.y));
                vec4 downColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b - 1) / resolution.y));
                if (currentColor.a > alpha || (leftColor.a > alpha && (upColor.a > alpha || downColor.a > alpha))) {
                    return false;
                }
            }
        } else {
            for (float i = currentPixelCoord.x - 1; i > lightPixelCoord.x; i -= 1) {
                vec4 currentColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b) / resolution.y));
                vec4 rightColor = texture(ourTexture, vec2((i + 1) / resolution.x, (i * a + b) / resolution.y));
                vec4 upColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b + 1) / resolution.y));
                vec4 downColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b - 1) / resolution.y));
                if (currentColor.a > alpha || (rightColor.a > alpha && (upColor.a > alpha || downColor.a > alpha))) {
                    return false;
                }
            }
        }
    // }
    // if (abs(lightPixelCoord.x - currentPixelCoord.x) > abs(lightPixelCoord.y - currentPixelCoord.y)) {
    //     float a = (currentPixelCoord.y - lightPixelCoord.y) / (currentPixelCoord.x - lightPixelCoord.x);
    //     float b = currentPixelCoord.y - a * currentPixelCoord.x;

    //     vec2 holocaust;
    //     vec2 direction;
    //     if (lightPixelCoord.x > currentPixelCoord.x) {
    //         holocaust = vec2(currentPixelCoord.x, lightPixelCoord.x);
    //         direction = vec2(1, 0);
    //     } else {
    //         holocaust = vec2(lightPixelCoord.x, currentPixelCoord.x);
    //         direction = vec2(-1, 0);
    //     }

    //     if (b < a + b) {
    //         direction = vec2(direction.x, 1);
    //     } else {
    //         direction = vec2(direction.x, -1);
    //     }

    //     for (float i = holocaust.x + 1; i < holocaust.y; i++) {
    //         vec4 currentColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b) / resolution.y));

    //         vec4 horColor;
    //         if (direction.x > 0) {
    //             vec4 horColor = texture(ourTexture, vec2((i - 1) / resolution.x, (i * a + b) / resolution.y));
    //         } else {
    //             vec4 horColor = texture(ourTexture, vec2((i + 1) / resolution.x, (i * a + b) / resolution.y));
    //         }

    //         vec4 verColor;
    //         if (direction.y > 0) {
    //             vec4 verColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b - 1) / resolution.y));
    //         } else {
    //             vec4 verColor = texture(ourTexture, vec2(i / resolution.x, (i * a + b + 1) / resolution.y));
    //         }

    //         if (currentColor.a > alpha || (horColor.a > alpha && verColor.a > alpha)) {
    //             return false;
    //         }
    //     }
    } else {
        // float a = 0;
        // if (currentPixelCoord.x != lightPixelCoord.x) {
        //     a = (currentPixelCoord.y - lightPixelCoord.y) / (currentPixelCoord.x - lightPixelCoord.x);
        // }
        // float b = currentPixelCoord.y - a * currentPixelCoord.x;

        // if (lightPixelCoord.y >= currentPixelCoord.y) {
        //     for (float i = currentPixelCoord.y + (1 / resolution.y); i < lightPixelCoord.y; i += (1 / resolution.y)) {
        //         vec4 currentColor = texture(ourTexture, vec2((i - b) / a, i));
        //         vec4 downColor = texture(ourTexture, vec2((i - b) / a, i - 1 / resolution.y));
        //         vec4 leftColor = texture(ourTexture, vec2((i - b) / a - 1 / resolution.x, i));
        //         vec4 rightColor = texture(ourTexture, vec2((i - b) / a + 1 / resolution.x, i));
        //         if (currentColor.a > alpha || (downColor.a > alpha && (leftColor.a > alpha || rightColor.a > alpha))) {
        //             return false;
        //         }
        //     }
        // } else {
        //     for (float i = currentPixelCoord.y - (1 / resolution.y); i > lightPixelCoord.y; i -= (1 / resolution.y)) {
        //         vec4 currentColor = texture(ourTexture, vec2((i - b) / a, i));
        //         vec4 upColor = texture(ourTexture, vec2((i - b) / a, i + 1 / resolution.y));
        //         vec4 leftColor = texture(ourTexture, vec2((i - b) / a - 1 / resolution.x, i));
        //         vec4 rightColor = texture(ourTexture, vec2((i - b) / a + 1 / resolution.x, i));
        //         if (currentColor.a > alpha || (upColor.a > alpha && (leftColor.a > alpha || rightColor.a > alpha))) {
        //             return false;
        //         }
        //     }
        // }
        return false;
    }
    return true;
}

void main() {
    //Get color from texture
    vec4 texColor = texture(ourTexture, fragTexCoord);

    //Skip pixel if it's not on the level layer
    if (texColor.a == 0) {
        // discard;
    }

    //Set pixel coordinates in correct resolution 320/180
    vec2 pos = vec2(gl_FragCoord.x, resolution.y - gl_FragCoord.y);
    
    // vec2 pixelCoord = vec2(floor(fragTexCoord.x * resolution.x + 0.5), floor(fragTexCoord.y * resolution.y + 0.5));
    // vec2 lightCoord = vec2((lightSource.x) / resolution.x, (lightSource.y) / resolution.y);
    vec2 lightCoord = vec2(lightSource.x + 0.5, lightSource.y + 0.5);
    
    // if (lightProps.y  - floor(sqrt((pow(lightCoord.x - fragTexCoord.x, 2) + pow(lightCoord.y - fragTexCoord.y, 2))) + 0.5) > 0) {
    //     colorVariance = CalculateColorVariance(texColor.a, lightSource, pixelCoord, colorVariance);
    // }
    if (CalculateColorVariance(texColor.a, lightCoord, pos)) {
        finalColor = vec4(texColor.x + 0.5, texColor.y + 0.5, texColor.z + 0.5, 1);
    } else {
        finalColor = vec4(texColor.xyz, 1);
    }
}