cmake_minimum_required(VERSION 3.5)
project(AlternateVoice VERSION 1.0.0)

file(GLOB SOURCES "src/*.cpp")

include_directories(include)

add_library(AlternateVoice SHARED ${SOURCES})

target_link_libraries(AlternateVoice enet)

add_definitions(-DALTERNATEVOICE_EXPORTS)
set(CMAKE_CXX_STANDARD 11)
