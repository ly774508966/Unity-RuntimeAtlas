##About
This project is intended to make displaying a large number of dynamically loaded texture assets more memory efficient. 
The project was built using Unity3D v4.3. To test the project, run the ```test``` scene.

##The Problem
While working on Crystal Casters we had to dynamically load and display a large number of 2D assets. The only efficient way 
to accompolish this is to pack all the assets into one or more atlases. Unity does have a [Texture2D.PackTexture](http://docs.unity3d.com/ScriptReference/Texture2D.PackTextures.html) method, 
but this method was of no use to us. The problem with [Texture2D.PackTexture](http://docs.unity3d.com/ScriptReference/Texture2D.PackTextures.html) is that
once you have packed a texture you can not easily add new textures to it. At the time no other runtime tools where available, so we had to make a new one.

##The solution
I implemented this texture packer based on a [Shadow Map packing technique](http://www.blackpawn.com/texts/lightmaps/) The CreateAtlas Method of AtlasCreator
stores all needed information for how a map is filled in without needing to store any of the actual textures. This allows the program to unload all assets
used in an atlas as soon as they are added to the atlas.

##NGUI
I stripped all NGUI related code out of this demo, but originally this packer was created for use with NGUI. The actual atlas packer is
generic enough to be used in any context outside of NGUI.
