 cmake_minimum_required(VERSION 3.16.1)

project("Nova")

file(GLOB_RECURSE SRC_FILES IO/*.h IO/*.cpp Core/*.h Core/*.cpp Members/*.h Members/*.cpp Statements/*.h Statements/*.cpp)
add_executable(Builder main.cpp ${SRC_FILES})