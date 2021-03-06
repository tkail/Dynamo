#Dynamo: Visual Programming for BIM#

## Description ##
The intent of this project is to provide a visual interface for building interesting parametric functionality on top of that already offered by Revit. Dynamo aims to be accessable both to the non-programmer and the programmer alike with the ability to visually script behavior and define your own nodes, but also the ability to write functionality using Python or by compiling .net code into dlls that can be linked at run time.

## Contributors ##

* [Ian Keough](https://github.com/ikeough): Started the project, main developer.
* [Stephen Elliott](https://github.com/Steell): Engine overhaul, main developer. 
* [Peter Boyer](https://github.com/pboyer): UX and UI overhaul, main developer.
* [Lev Lipkin](https://github.com/LevL): Revit node designer, Revit interaction specialist.
* [Matt Jezyk](https://github.com/tatlin): Node designer and overall design input.
* [Zach Kron](https://github.com/kronz): UX and node design input.
* Lillian Smith: Useful feedback and use cases.
* [Tom Vollaro](https://github.com/tvollaro): Useful feedback and use cases.


Dynamo has been developed based on feedback from several parties inlcuding Buro Happold Engineers, Autodesk, and students and faculty at the USC School of Architecture.


## Running Dynamo ##

The current version will run on top of Revit 2013, Revit 2014, Project Vasari Beta 2, and Project Vasari Beta 3. 

## Releases ##

###0.5.2###

July 2013

Fixes
- UV Grid improvements
- Particle System and Dynamic Relaxation improvements
- Delete for notes
- Formula node is now case insensitive
- Watch and Watch 3d improvments
- Zoom and workspace tabbing
- Length node culture formatting
- Fixed rectangle node
- Custom node fixes
- Multi-output node fixes

New
- Structural Framing nodes
- More List management nodes, Flatten changes, Repeat node
- More Math Nodes
- Node alignment tools 
- Best Fit Plane, Best Fit Arc
- Number Slider state display
- Last computed value is now displayed on nodes with mouse-over
- Zoom state saved with workspaces
- More nodes for using Analysis Visualization Framework (model coloring)
- Color Nodes (Hue, Brightness, Saturation, etc)
- Better node error state reporting
- Curves Through Points node
- Find Nodes from Selected Elements
- Watch node shows index of list members
- Tooltips in search

Known Issues
- See Issues on Github:  https://github.com/ikeough/Dynamo/issues?state=open

###0.5.0###

June 2013

Known Issues
- Watch Node results disappear when switching views to a different workspace tab. Issue #156
- Cannot cancel out of a face selection operation.  Issue # 155
- Noise Field and Color Brightness Nodes Cannot Recieve List Inputs #150
- "XYZ Array on Curve" Node does not create point on end of curve.  #138
 
Fixes
- Better crash handling
- Fixed preview geometry crashes
- Python script node crash fixes
- Save as saves to new location properly
- Custom node creation fixes (save as, reopen, recursive node issues)
- Fixed Write CSV File node
- Loading files with missing custom nodes creates a proxy node indicating the node they are replacing
- Sliders update values properly when range is changed
- Copy/Paste fixes
- Evaluate curve or edge no longer crashes Watch 3d
- Better handling of escape characters in string nodes
- Better handling of localized handling of comma and period decimal delimiters
- Nodes using selection remember associations after save and reopen
- List not first input is now index0
- Fixed Build Sequence node crash
 

New
- Combined installer for Revit 2013, 2014, and Vasari Beta 2 and 3
- Better background contrast with node wires and other visibility improvements
- Improved handling of lists by many nodes
- Dyn file load time improvements
- New Tessellate nodes:  Convex Hull 3d , Delaunay on Face and 3d, Voronoi on Face
- Background preview controls
- More solid and curve creation nodes
- String nodes now handle returns, tabs, and other escape characters
- Compose Functions Node (combine two single parameter functions into one)
- Filter Out Node (removes a given predicate from a list)
- Transpose List Node (swaps rows and columns in a list of lists)
- Combine operates on uneven lists
- Copy/Paste copies node state
- Length node that uses Project Units (metric and imperial)
- Preview visibility controls in right click for each node



###0.4.0###

June 2013

Known Issues
- Cannot use comma as a decimal separator
- Node Preview state is not saved with dyn file
- Transform Point does not respond to rotation input

Fixes
- Can now run Python scripts from disk location and embedded
- Autocomplete for Python now works in Vasari
- Deluany Tessellation node is working 
- Preview 3d geometry in the view background refreshing properly
- Formula node can now copy paste
- Fixed crash bug with connecting node to itself. This also corrects the double click crash on a port.
- Fix XYZFromReferencePoint so that it returns an XYZ.
- Fixed behavior where lacing and element count would not coincide, sometimes leaving an element behind.
- CurveByPointsByLine no longer duplicates points.
- Fixes for Adaptive Component placement
- Better crash handling
- Fixed multiple crash issues around deleting elements created by graph
- Auto-Mapping (lacing)improvments
- Selection nodes improved save/open fidelity
- Experimental nodes for Isometric View and Section Box generation
- Fixed Crash Saving Image

New
- Wiring new inputs will push out old inputs
- Iron Python 2.7.3 bundled with install
- OffsetCurves node
- Update CurveByPoints node to allow for creation of reference lines
- Reference Curve Node
- More list management nodes: sublist, slice,  flatten
- Preview geometry is now controlled on a per-node basis (right click menu)
- Compute curve derivatives
- Open file defaults to sample directory
- UI improvements and cleanup
- Cache previous position of Dynamo window, do not maximize on startup
- Add XYZStartEndVector, CrossProduct, Negate, and Average
- Normalize the vectors when creating a transform
- Lacing is set to "Longest" by default
- Angle Node that takes degree inputs
- Updates, fixes, and culling of Samples
- Nodes for CurveLoop from Curves, Curve List from Curve Loop
- Nodes for Revolve and Sweep for Solid Creation


###0.3.4###

May2013

Known Issues
- Cannot run Python scripts from disk location (embedded is fine)
- Delauny Tesselation node is always passing an empty list
- Preview 3d geometry in the view background is not consistently refreshing
- Formula node crashes when copy pasting when running in auto


Fixes
- Dynamo now works alongside Revit Python Shell and other conflicting addins
- Solar Radiation example works
- More stability


New and Updated Nodes
- Adaptive Components
- Formulas using N-Calc syntax
- Compute Face Derivatives
- Project Points on Curves and Faces
- Extract Solids, Faces, and Edges from elements
- Evaluation of Curves and Edges
- Selection of Imports, Host objects, Edges, Faces and Solids from Element
- Watch3d improvements
- Python node Autocomplete (in progress and only in Revit)

Functionality and UI
- Appearance Cleanup
- Preview geometry in Dynamo Background 
- Category and Node Browsing Improvements
- Ability to pass lists into nodes (Lacing and auto-mapping of lists)
- New Node appearance
- Graph retains memory and parametric control of elements created in previous sessions
 

Engineering
- MVVM standardization

Samples
- Adaptive Component Placement
- Face Extraction from Solids
- Formulas
- Create Point Sequence
- Some existing sample cleanup


###0.3.0###

April 2013

Known Issues
- Dynamo cannot start with Revit Python Shell Installed.
- Dynamo continues regenerating revit elements after closing when "Run Automatically" is checked
- Face selection of loaded families move origin to Revit 0,0,0
- Solar Radiation Optimization Sample may lock CSV file and stop running


New or Updated nodes
- Watch 3d geometry preview
- Expanded Curve drawing nodes
- Transforms
- Vectors
- More Analysis capabilities
- Delaunay Tessellation
- Dynamic Relaxation 
- Leap Motion interface
- Drafting view
- Syntax coloring for Python Node Editor
- Random Number generator
- Simplex Fields
- Read Image from File node
- Arduino node is more robust
- UDP node for responding to input over network 
- UV Grids
- Surface Domain nodes
- More math nodes (modulo and exponent)
- Height node
- Dynamic Relaxation improvments

UI:
- Custom Node creation from Selection
- Multiple Outputs from custom nodes
- Cut and Paste 
- Box and Multi Select
- Search/Browse: Scalable browsing, more robust search
- Lacing: Data Matching for Lists
- Application Menu standardization
- Spline or Polyline Connector option
- More legible and compact node display

Engineering:
- Code Cleanup
- Expanded code Documentation
- Code Separation (Disentangle UI, Engine, Revit code)
- API reflection:  Ability to automatically generate nodes from the API
- Automated testing for improved stability

Samples:
- Dynamic Relaxation
- Curves
- Arduino
- CSV driving point creation
- Tesselation
- Transforms




###0.2.0###

November 2012

New or updated nodes:

- Added slider value display when slider is moved
- Added XYZ utility nodes (XYZ Zero, XYZ Basis, XYZ Scale, XYZ Add, Evaluate Normal, Evaluate XYZ)
- Added XYZ Number Extractor nodes (XYZ -> X, XYZ -> Y, XYZ -> Z)
- Added Reference Point By Normal node
- Fixed Arduino node, it now works with new framework
- Added Execution Interval (Timer) node
- Added Spatial Field Manager node and Analysis Results node
- Added Level node and Level by Selection node
- Added Planar Nurbs Spline node
- Added Divided Surface node
- Added Divided Path node
- Updated Lofted Form node to handle surfaces vs solids and solids vs voids
- Added Chomp, Dice, LaceForward and LoacBackwards definitions for working with 2D arrays.
- Added new string utility nodes 
- Added a CSV file writer and basic file writer nodes
- Added a Watch node
- Added Dynamic Relaxation node (still experimental)

UI:
- Added abilty to create Notes in graphics window
- The built-in samples in File/Samples have been reorganized to use subfolders
- added File/Save command
- Pressing the Dynamo Ribbon button in Revit/Vasari while Dynamo is already running now simply makes Dynamo window visible again
- It is now possible to move around the graphics screen with the arrow keys.
- If second monitor is available, Dynamo will maximize to it.

###0.1.0###

June 2012

- User Interface - side panel to allow you to search and then drag and drop new nodes in to place
- User-created nodes - you can make 'sub-nodes' and then reference them elsewhere, these act like writing a reusable function in a programming language
- Python Scripting node - we have integrated a python scripting node into 
- Math, logic and other nodes - many new nodes added to support evaluation,  iteration and looping

Dynamo For Vasari Beta 2 WIP: Integrating Dynamo into Project Vasari and extending Dynamo to support integrated analysis and performance-based design.


## Dynamo License ##

Those portions created by Ian are provided with the following copyright:

Copyright 2013 Ian Keough

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

## Third Party Licenses ##

###Avalon Edit###
http://www.codeproject.com/Articles/42490/Using-AvalonEdit-WPF-Text-Editor  
http://opensource.org/licenses/lgpl-3.0.html  

###Kinect for Windows###
http://www.microsoft.com/en-us/kinectforwindows/  
http://www.microsoft.com/en-us/kinectforwindows/develop/sdk-eula.aspx  

###Helix3D###
https://helixtoolkit.codeplex.com/  
https://helixtoolkit.codeplex.com/license  

###Iron Python###
http://ironpython.net/  
http://opensource.org/licenses/apache2.0.php  

###MiConvexHull###
http://miconvexhull.codeplex.com/  
http://miconvexhull.codeplex.com/license  

###NCalc###
http://ncalc.codeplex.com/  
http://ncalc.codeplex.com/license  

###NUnit####
http://www.nunit.org/  
http://www.nunit.org/index.php?p=license&r=2.6.2  

###Prism###
http://msdn.microsoft.com/en-us/library/gg406140.aspx  
http://msdn.microsoft.com/en-us/library/gg405489(PandP.40).aspx  
