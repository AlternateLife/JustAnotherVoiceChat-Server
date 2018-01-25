cmake_minimum_required(VERSION 3.5)
project(AlternateVoice VERSION 1.0.0)

file(GLOB SOURCES "src/*.cpp")

include_directories(include)

add_library(AlternateVoice SHARED ${SOURCES})

add_definitions(-DALTERNATEVOICE_EXPORTS)
