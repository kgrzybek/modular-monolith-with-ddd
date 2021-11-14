# Modularity

Modular programming definition from [Wikipedia](https://en.wikipedia.org/wiki/Modular_programming):
> Modular programming is a software design technique that emphasizes separating the functionality of a program into independent, interchangeable modules, such that each contains everything necessary to execute only one aspect of the desired functionality. A module interface expresses the elements that are provided and required by the module. The elements defined in the interface are detectable by other modules. The implementation contains the working code that corresponds to the elements declared in the interface.

A system can be devided to modules in defferent ways(using different creterias).  D.L. Parnas in the [paper](https://www.win.tue.nl/~wstomv/edu/2ip30/references/criteria_for_modularization.pdf) says that the right creteria is **information hiding** and it provides independence, interchangability of modules and comprehesibility of modules and a system as a whole.
> ..."module" is considered
to be a **responsibility assignment** rather than a subprogram. The modularizations include the design decisions which must be made before the work on independent modules can begin.

>Every module in the second decomposition is characterized by its knowledge of a **design decision which it hides from all others**. Its interface or definition was chosen to reveal as little as possible about its inner workings.
 

