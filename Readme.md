
# What is Nova

> Nova is a cross plateform and high level abstraction interpreted language written in C++. 
  
  ###### Paradigms:
  + Interpreted (only for now)
  + Structured
  + Imperative
  + Object-oriented
  + Functional
  + Nova grammar is regular
  ###### Example:

  ```
using nova
using nova.math

namespace myProgram

class MyClass
{
    // This code is written in Nova !
    int main()
    {
       Nova.print("hello world")

       int var1 = 2
       int var2 = -7
       Nova.print(var1 * Math.Abs(var2))
    }
}
 ```
 ###### How is the language working ?
 + Once your nova file(s) source code is written. Just give it to the Builder 
 + The builder will generate a .nov file (this is an assembly)
 + Give the .nov to the interpreter and it will run your assembly (from main() entry point)
 > all .nov files in the executing directory are linked as referenced assemblies to the current assembly.

###### Builder

 ![builder](https://puu.sh/F2hEj/67193bd504.png)

 ###### How to run
  + git clone https://github.com/Skinz3/Nova.git
  + make ./Builder
  + make ./Interpreter
  + ``` ./builder myScript.nv myAssembly.nov ```  ---> myScript.nv must have a main point entry.
  + ``` ./interpreter myAssembly.nov ```

## Contacts

  > My discord is: **Skinz#1128**
  
