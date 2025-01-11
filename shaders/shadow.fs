#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;
layout(origin_upper_left) in vec4 gl_FragCoord;

uniform sampler2D Texture;
uniform vec2 lightProps;
uniform vec2 lightSource;

out vec4 finalColor;

int Alpha(float var1) {
    return int(floor(var1 * 20 + 0.5));
}

float Distance(ivec2 var1, ivec2 var2) {
    return sqrt(pow(var1.x - var2.x, 2) + pow(var1.y - var2.y, 2));
}

int Direction(int var1, int var2) {
    if (var1 > var2) {
        return 1;
    } else if (var1 < var2) {
        return -1;
    } else {
        return 0;
    }
}

float Shadow(int pixelAlpha, ivec2 lightLocation, ivec2 pixelLocation) {
    float a = float(pixelLocation.y - lightLocation.y) / (pixelLocation.x - lightLocation.x);
    float b = pixelLocation.y - a * pixelLocation.x;
    
    ivec2 obstacleLocation, variance = ivec2(Direction(lightLocation.x, pixelLocation.x), Direction(lightLocation.y, pixelLocation.y));
    int alphaDifference, strength, horizontalDifference, verticalDifference, side = 0;

    float lasty = pixelLocation.y - 0.5 * variance.y;

    if (pixelLocation.x == lightLocation.x) {
        side = -1;
        // float i = pixelLocation.x;
        // float y = floor(i * a + b + 0.5) - 0.5 * variance.y;
        // while (y != lasty) {
        //     obstacleLocation = ivec2(int(floor((lasty + variance.y - b) / a + 0.5)), int(lasty + variance.y + 0.5 * variance.y));

        //     int possibleObstacleAlpha = Alpha(texelFetch(Texture, obstacleLocation, 0).a);

        //     if (possibleObstacleAlpha > pixelAlpha) {
        //         alphaDifference = possibleObstacleAlpha - pixelAlpha;
        //         side = 1;
        //     }

        //     lasty += variance.y;

        //     if (side == 1) {
        //         ivec2 newObstacleLocation = ivec2(int(i + 0.5 * variance.x), int(floor(i * a + b + 0.5)));

        //         if (obstacleLocation == newObstacleLocation) {
        //             int possibleObstacleAlpha = Alpha(texelFetch(Texture, obstacleLocation, 0).a);

        //             if (possibleObstacleAlpha > pixelAlpha) {
        //                 alphaDifference = possibleObstacleAlpha - pixelAlpha;

        //                 if (Alpha(texelFetch(Texture, ivec2(obstacleLocation.x, obstacleLocation.y + 1), 0).a) > pixelAlpha || Alpha(texelFetch(Texture, ivec2(obstacleLocation.x, obstacleLocation.y - 1), 0).a) > pixelAlpha) {
        //                     side = -1;
        //                     if (Alpha(texelFetch(Texture, ivec2(obstacleLocation.x + 1 * variance.x, obstacleLocation.y), 0).a) > pixelAlpha && Alpha(texelFetch(Texture, ivec2(obstacleLocation.x, obstacleLocation.y + 1), 0).a) <= pixelAlpha && Alpha(texelFetch(Texture, ivec2(obstacleLocation.x, obstacleLocation.y - 1), 0).a) <= pixelAlpha) {
        //                         side = 3;
        //                     }
        //                 } else {
        //                     if (Alpha(texelFetch(Texture, ivec2(obstacleLocation.x, obstacleLocation.y + 1 * variance.y), 0).a) > pixelAlpha && Alpha(texelFetch(Texture, ivec2(obstacleLocation.x + 1, obstacleLocation.y), 0).a) <= pixelAlpha && Alpha(texelFetch(Texture, ivec2(obstacleLocation.x - 1, obstacleLocation.y), 0).a) <= pixelAlpha) {
        //                         side = 3;
        //                     }
        //                 }
        //                 // if (Alpha(texelFetch(Texture, ivec2(obstacleLocation.x + 1, obstacleLocation.y), 0).a) > pixelAlpha || Alpha(texelFetch(Texture, ivec2(obstacleLocation.x - 1, obstacleLocation.y), 0).a) > pixelAlpha) {
        //                 //     side = 1;
        //                 // }
        //                 break;
        //             }
        //         } else {
        //             if (Alpha(texelFetch(Texture, ivec2(obstacleLocation.x, obstacleLocation.y + 1 * variance.y), 0).a) > pixelAlpha && Alpha(texelFetch(Texture, ivec2(obstacleLocation.x + 1, obstacleLocation.y), 0).a) <= pixelAlpha && Alpha(texelFetch(Texture, ivec2(obstacleLocation.x - 1, obstacleLocation.y), 0).a) <= pixelAlpha) {
        //                 side = 3;
        //             }
        //             break;
        //         }
        //     }
        // }
    } else {
        for (float i = pixelLocation.x + 0.5 * variance.x; i != lightLocation.x - 0.5 * variance.x; i += variance.x) {
            float y = floor(i * a + b + 0.5) - 0.5 * variance.y;
            while (y != lasty) {
                obstacleLocation = ivec2(int(floor((lasty + variance.y - b) / a + 0.5)), int(lasty + variance.y + 0.5 * variance.y));

                int possibleObstacleAlpha = Alpha(texelFetch(Texture, obstacleLocation, 0).a);

                if (possibleObstacleAlpha > pixelAlpha) {
                    alphaDifference = possibleObstacleAlpha - pixelAlpha;
                    side = 1;
                }

                lasty += variance.y;

                if (side == 1) {
                    ivec2 newObstacleLocation = ivec2(int(i + 0.5 * variance.x), int(floor(i * a + b + 0.5)));

                    if (obstacleLocation == newObstacleLocation) {
                        int possibleObstacleAlpha = Alpha(texelFetch(Texture, obstacleLocation, 0).a);

                        if (possibleObstacleAlpha > pixelAlpha) {
                            alphaDifference = possibleObstacleAlpha - pixelAlpha;

                            if (Alpha(texelFetch(Texture, ivec2(obstacleLocation.x, obstacleLocation.y + 1), 0).a) > pixelAlpha || Alpha(texelFetch(Texture, ivec2(obstacleLocation.x, obstacleLocation.y - 1), 0).a) > pixelAlpha) {
                                side = -1;
                                if (Alpha(texelFetch(Texture, ivec2(obstacleLocation.x + 1 * variance.x, obstacleLocation.y), 0).a) > pixelAlpha && Alpha(texelFetch(Texture, ivec2(obstacleLocation.x, obstacleLocation.y + 1), 0).a) <= pixelAlpha && Alpha(texelFetch(Texture, ivec2(obstacleLocation.x, obstacleLocation.y - 1), 0).a) <= pixelAlpha) {
                                    side = 3;
                                }
                            } else {
                                if (Alpha(texelFetch(Texture, ivec2(obstacleLocation.x, obstacleLocation.y + 1 * variance.y), 0).a) > pixelAlpha && Alpha(texelFetch(Texture, ivec2(obstacleLocation.x + 1, obstacleLocation.y), 0).a) <= pixelAlpha && Alpha(texelFetch(Texture, ivec2(obstacleLocation.x - 1, obstacleLocation.y), 0).a) <= pixelAlpha) {
                                    side = 3;
                                }
                            }
                            // if (Alpha(texelFetch(Texture, ivec2(obstacleLocation.x + 1, obstacleLocation.y), 0).a) > pixelAlpha || Alpha(texelFetch(Texture, ivec2(obstacleLocation.x - 1, obstacleLocation.y), 0).a) > pixelAlpha) {
                            //     side = 1;
                            // }
                            break;
                        }
                    } else {
                        if (Alpha(texelFetch(Texture, ivec2(obstacleLocation.x, obstacleLocation.y + 1 * variance.y), 0).a) > pixelAlpha && Alpha(texelFetch(Texture, ivec2(obstacleLocation.x + 1, obstacleLocation.y), 0).a) <= pixelAlpha && Alpha(texelFetch(Texture, ivec2(obstacleLocation.x - 1, obstacleLocation.y), 0).a) <= pixelAlpha) {
                            side = 3;
                        }
                        break;
                    }
                }
            }

            if (side != 0) {
                break;
            }
            
            obstacleLocation = ivec2(int(i + 0.5 * variance.x), int(floor(i * a + b + 0.5)));

            int possibleObstacleAlpha = Alpha(texelFetch(Texture, obstacleLocation, 0).a);

            if (possibleObstacleAlpha > pixelAlpha) {
                alphaDifference = possibleObstacleAlpha - pixelAlpha;
                side = -1;
                if (Alpha(texelFetch(Texture, ivec2(obstacleLocation.x + 1 * variance.x, obstacleLocation.y), 0).a) > pixelAlpha && Alpha(texelFetch(Texture, ivec2(obstacleLocation.x, obstacleLocation.y + 1), 0).a) <= pixelAlpha && Alpha(texelFetch(Texture, ivec2(obstacleLocation.x, obstacleLocation.y - 1), 0).a) <= pixelAlpha) {
                    side = 0;
                }
                break;
            }
        }
    }

    if (abs(obstacleLocation.x - lightLocation.x) <= alphaDifference) {
        horizontalDifference = abs(obstacleLocation.x - lightLocation.x);
    } else {
        horizontalDifference = alphaDifference;
    }
    if (abs(obstacleLocation.y - lightLocation.y) <= alphaDifference) {
        verticalDifference = abs(obstacleLocation.y - lightLocation.y);
    } else {
        verticalDifference = alphaDifference;
    }

// && degrees(atan(abs(a))) > 2

    if (side == 1) {
        if (abs(pixelLocation.y - obstacleLocation.y) <= verticalDifference && abs(pixelLocation.x - obstacleLocation.x) <= horizontalDifference) {
            strength = 10;
        }
    } else if (side == -1) {
        if (abs(pixelLocation.x - obstacleLocation.x) <= horizontalDifference && abs(pixelLocation.y - obstacleLocation.y) <= verticalDifference) {
            strength = 10;
        }
    } else {
        strength = 0;
    }
    return strength;
}

void main() {
    vec4 pixelColor = texture(Texture, fragTexCoord);
    ivec2 lightLocation = ivec2(lightSource.xy);
    ivec2 pixelLocation = ivec2(gl_FragCoord.xy);

    //Skip pixel if it's not on the level layer
    // if (pixelColor.a == 0) {
    //     discard;
    // }
    
    // distance is from 0 to 367
    // int distance = Distance(lightLocation, pixelLocation);

    float strength = Shadow(Alpha(pixelColor.a), lightLocation, pixelLocation);

    if (strength > 0) {
        finalColor = vec4(pixelColor.xyz - strength / 100, 1);
    } else {
        finalColor = vec4(pixelColor.xyz, 1);
        // finalColor = vec4(pixelColor.xyz + (0.1 - float(distance / 3670)), 1);
    }
}