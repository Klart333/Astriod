Astriod Tool

Tool can be found under Tools/Control panel
(Script path: Scripts/Tool/ControlPanel)

The tool is a sort of control panel where the designer can easily in one place adjust all of the core variables in the game. 
The spawning and the shooting are adjustable, if it was a real tool people would use I would add more features to adjust.
To add more you just copy-paste the code and connect it to another scriptable object, this was also the reason I didn't, 
it's just quite boring and I hope that isn't a problem. 

For VG
•Utilize advanced features of Unity editor tools, such as custom inspectors or editors, to enhance the user experience and make the tool more powerful and efficient. 
 - Included, the tool resides inside it's one editor window, which can be dragged around and adjusted
   just like any other window, which should be more user-friendly as opposed 
   to be forced to have the object selected for a custom inspector.
   This is done in the ControlPanel script with the GetWindow function, inhereted from EditorWindow

•Incorporate error handling, undo/redo functionality, and other best practices to improve the robustness and reliability of the tool.
 - Included, all changes done within the control panel are recorded and added to unity's undo system. There is almost nothing 
   more annoying than when you're trying to undo something, and then instead of undoing the change it undos the selection. 
   This I did not want. All the fields are done through SerializedProperties which are unity's recommended way of doing 
   custom exposed properties, so all unity functionality is included, undo recording, error handling and 
   saving the values properly (setting as dirty etc.)

•Create a well-organized and easy-to-user interface, making sure that all functionalities are intuitive and easy to find
 - Included, the tool is never outside of hands reach and right now because there isn't too much to it it's very easy 
   to find what you are looking for as everything is titled properly. If I added a lot more variables I would
   maybe include tabs to seperate different areas, which I looked into a little.

•Reflect on the process of creating the tool and provide insightful and thoughtful feedback on how the use of ScriptableObjects and Unity editor tools impacted the development process.
 - I regularly use scriptable objects in my projects to easily store data and sometimes even functionality that doesn't 
   need to live in the scene and is reapetedly used. In which case it can be quite handy in composition patterns but anyway. 
   It was very easy setting up the tool as the scriptable obejcts where already being used, I just needed to 
   edit their values and the changes would take place in game even in runtime. I don't really like loading them 
   through Resources, and a better system can be implemented I'm sure, but it's very much not a problem as I see it
   so going through extra effort to make something functionaly identical wasn't really that tempting.

   Incase you want an short essay on scriptable objects, this is what chat-gpt gave me: 
   ScriptableObjects and Unity editor tools can have a significant impact on the development process by allowing for a more 
   organized and efficient workflow. ScriptableObjects, in particular, can be used to store data that is shared across 
   multiple objects or scenes, making it easy to manage and modify that data in one central location. 
   Additionally, by making use of Unity editor tools such as custom inspectors and property drawers, 
   developers can create a more user-friendly and intuitive interface for editing and manipulating that data.

   One potential benefit of using ScriptableObjects is that they can be easily serialized and saved to disk, 
   allowing for greater flexibility in terms of version control and collaboration. 
   Additionally, by separating data from behavior, it can make it easier to reason about the overall structure of the project
   and make changes without worrying about unintended side effects.

   However, it's important to keep in mind that the use of ScriptableObjects and Unity editor tools can also 
   add complexity to the development process, particularly if they are used excessively or in a way 
   that is not well-organized. It's important to have a clear understanding of how the data is being used
   in the project and have a plan for how it will be managed.

   Overall, the use of ScriptableObjects and Unity editor tools can be a powerful way to improve the development process,
   but it's important to use them in a thoughtful and organized manner.

Thanks for reading
// John Roddis

   