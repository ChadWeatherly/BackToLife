# BackToLife

These should be all the files that we have for our game project. Below will be a quick rundown of how to use GitLab.

GitLab is a website that helps with "Git" version control, which is basically a protocol/set of rules to follow that enables us to work side-by-side on the same project. The goal is for each new item you work on to be its own branch, named based on what you did (say, like player_movement or something).

GitKraken is a little wonky, but it seems to work. You'll see on the left side that there are two drop-down menus, __Local__ and __Remote__. The Local files are what files are on your personal computer for each branch, while the Remote tab shows files that are the ground truth of the whole repository for each branch. You'll see that the main branch is under the __origin__ tab, and the origin tab is ground truth, or base, of the code. This is the code we all agree is the most up-to-date and is stored on the cloud. The way you code is thus:

1. Switch the branch at the top-left to the one you want to create (To create, I found you can right-click on the Local/main branch and create a child branch, which will duplicate the main branch). Git keeps track of the different branches on your computer, so if you change branches, it will change the files in that location to the ones on that branch, which means you need to exit out of the Unity editor and re-click on the game workspace because the files have changed.
2. Make sure you have hit the "Pull" button for the branch you're on (top left of GitKraken). This will make sure that whatever changes to the origin/branch have been made will be downloaded to your computer.
3. Do the work and changes you want for that branch. You'll notice that after you've saved some files, that your branch name will have 2 spots in the graph. This indicates that the files in your local branch are different than the files on the origin (Remote) version of the branch.
4. Once you're satisfied with your changes, on the right side of GitKraken, you'll hit "Stage changes" which prepares them to be uploaded.
5. Add a description and comment on the bottom of that tab, then hit "Commit Changes".
6. Finally, Hit the "Push" button at the top of GitKraken. This will overwrite the branch on the origin/Remote with your modified branch that you've just now created.
7. When you're confident that the changes you want on that branch are what you want, then we can merge that branch into main, making it so that this is our new working version. Merging is a bit more complicated, so just let Chad know when you want to merge a branch and he can do it. But also know that everyone will have to continue working off that merge.
