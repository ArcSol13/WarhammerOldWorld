# Warhammer-OldWorld

## Creating and adding a new module

Using the ModuleManager, creating a SubModule should follow the same pattern that you would follow while making your own separte module, barring one difference: inherit from CustomSubModule rather than MBSubModuleBase.

Once your SubModule is created, add it to the activeModules list in the ModuleManager.

Once this is done, your module will be utilized the same way it would be independently, as the module manager will call all MBSubModuleBase methods for each module in the activeModules list.
