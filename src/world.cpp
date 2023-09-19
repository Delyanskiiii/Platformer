#include <iostream>
#include <fstream>
#include "world.h"

World::World() {
    // Create an instance of the custom struct
    Person person;
    person.name = "John";
    person.age = 30;

    // // Save the struct data to a file
    // std::ofstream outFile("person_data.txt", std::ios::binary);

    // if (!outFile.is_open()) {
    //     std::cerr << "Failed to open the file for writing." << std::endl;
    //     // return 1;
    // }

    // outFile.write(reinterpret_cast<char*>(&person), sizeof(person));
    // outFile.close();

    // Read the struct data from the file
    Person loadedPerson;

    std::ifstream inFile("person_data.txt", std::ios::binary);

    if (!inFile.is_open()) {
        std::cerr << "Failed to open the file for reading." << std::endl;
        // return 1;
    }

    inFile.read(reinterpret_cast<char*>(&loadedPerson), sizeof(loadedPerson));
    inFile.close();

    // Display the loaded data
    std::cout << "Name: " << loadedPerson.name << std::endl;
    std::cout << "Age: " << loadedPerson.age << std::endl;

    // return 0;
}
