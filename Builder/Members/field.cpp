#include "field.h"

#include <iostream>

Field::Field(ModifierEnum modifier, string name, string type, Statement *value)
{
    this->modifier = modifier;
    this->name = name;
    this->type = type;
    this->value = value;
}