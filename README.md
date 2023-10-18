# BackToLife

These should be all the files that we have for our game project. Below will be a quick rundown of how to use GitHub.

GitHub is a website that helps with "Git" version control, which is basically a protocol/set of rules to follow that enables us to work side-by-side on the same project. The goal is for each new item you work on to be its own branch, named based on what you did (say, like player_movement or something).

The Local files are what files are on your personal computer for each branch, while the Remote tab shows files that are the ground truth of the whole repository for each branch. You'll see that the main branch is under the __origin__ tab, and the origin tab is ground truth, or base, of the code. This is the code we all agree is the most up-to-date and is stored on the cloud. The way you code is thus:

__Steps when Working__

Note: Everything can be done within the GitHub desktop app, which is easier. I've removed git lfs for this repo. 

_Before even beginning:__ Totally remove/delete your file folder where the game project files are located. Through either github.com or the desktop app, Clone the repository to your machine, which will download this repository to your computer. It will ensure that all the settings are caught up for git lfs and the .gitignore so that Library files aren't added.

1. As before, we have branches, where _master_ is the working version of our project. In this case, when working on something new, create a new branch based off _master_, naming it based on what you're doing (lighting, enemy_movement, etc.)
2. Within that branch, you can change whatever you want. There are always two versions of that branch, the local version (on your computer) and the remote version (what everyone else sees). As you work locally, your local version will be different than the remote version. You will commit and push your changes to the remote to keep it up to date.
3. Within your branch, whatever level you are working on, make a dummy copy of that level, with a name based on what you're doing. This makes sure you don't mess up the whole level but things can be merged easier for me (Chad) later on. So, you'll duplicate the level you're working on(say, Level1) and rename this new asset Level1_lighting , or whatever you want. Just make sure it's easy to track what you're doing.
4. After you're finished, I will merge with master to make sure things work. _NEVER_ merge to master yourself.

All of this should make it simpler when working with these files and things. Hopefully, no more git issues. 
