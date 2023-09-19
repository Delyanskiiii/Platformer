import struct

# Define the Person struct
class Person:
    def __init__(self, name, age):
        self.name = name
        self.age = age

# Create an instance of the Person struct
person = Person("John", 30)

# Serialize and save the struct data to a binary file
with open("person_data.txt", "wb") as file:
    # Serialize the struct members
    name_bytes = person.name.encode("utf-8")  # Encode the string as bytes
    age = person.age

    # Pack the data using struct format
    packed_data = struct.pack(f"{len(name_bytes)}sI", name_bytes, age)

    # Write the packed data to the file
    file.write(packed_data)
