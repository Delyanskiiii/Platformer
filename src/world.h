#pragma once

class World
{
    public:
        World();
        void Update();
        void Draw();
    // private:

};


struct Person {
    std::string name;
    int age;
};
// struct Level {
//     Block blocks[];
// };

struct Block {
    bool exists;
    int pixels[16];
};