#include <iostream>
#include <raylib.h>
#include <fstream>
#include "world.h"
#include <sstream>
#include <vector>
#include <algorithm>

// bool CompareBlocks(const Block& a, const Block& b) {
//     if (a.x == b.x) {
//         return a.y < b.y;
//     }
//     return a.x < b.x;
// }

extern int diff;

World::World() {
    std::ifstream inFile("levels/1.txt");

    if (inFile.is_open()) {
        // Read existing data from the file
        std::string line;
        while (std::getline(inFile, line)) {
            std::istringstream iss(line);
            Block loadedBlock;

            // Parse x and y
            if (!(iss >> loadedBlock.x >> loadedBlock.y)) {
                std::cerr << "Error parsing line: " << line << std::endl;
                continue; // Skip this line if parsing fails
            }

            // Parse 100 pixels
            for (int i = 0; i < 100; ++i) {
                if (!(iss >> loadedBlock.pixels[i].red >> loadedBlock.pixels[i].green >> loadedBlock.pixels[i].blue >> loadedBlock.pixels[i].level)) {
                    std::cerr << "Error parsing pixels for Block at (" << loadedBlock.x << ", " << loadedBlock.y << ")" << std::endl;
                    break; // Stop parsing pixels if an error occurs
                }
            }

            // Add the loaded block to the vector
            blocks.push_back(loadedBlock);
        }
        inFile.close();
    }

    // // Create a new data point and add it to the vector
    // DataPoint newDataPoint;
    // newDataPoint.x = 1;
    // newDataPoint.y = 3;
    // newDataPoint.colors[0] = 255; // Red
    // newDataPoint.colors[1] = 128; // Green
    // newDataPoint.colors[2] = 0;   // Blue

    // dataPoints.push_back(newDataPoint);

    // // Sort the data points by x and then by y
    // std::sort(dataPoints.begin(), dataPoints.end(), CompareBlocks);

    // // Open the file for writing
    // std::ofstream outFile("data_points.txt");

    // if (!outFile.is_open()) {
    //     std::cerr << "Failed to open the file for writing." << std::endl;
    //     // return 1;
    // }

    // // Write the sorted struct data to the text file on separate lines
    // for (const auto& dp : dataPoints) {
    //     outFile << dp.x << " " << dp.y << " " << dp.colors[0] << " "
    //             << dp.colors[1] << " " << dp.colors[2] << std::endl;
    // }
    // outFile.close();

    // // Display the sorted loaded data
    // for (const auto& dp : dataPoints) {
    //     std::cout << "x: " << dp.x << " y: " << dp.y << " Colors: "
    //               << dp.colors[0] << " " << dp.colors[1] << " " << dp.colors[2] << std::endl;
    // }

    // return 0;
}

void World::Draw() {
    for (const auto& block : blocks) {
        for (int i = 0; i < 100; ++i) {
            // std::cout << block.x * diff * 10 + i % 10 << std::endl;
            DrawRectangle(block.x * diff * 10 + i % 10 * diff, block.y * diff * 10 + i / 10 * diff, 1 * diff, 1 * diff, Color{block.pixels[i].red, block.pixels[i].green, block.pixels[i].blue, 255});
        }
    }
}
