game available to play at: https://20079934.github.io/3DGDA1/

# Controls
- WASD to move
- Mouse to look around

# Features
### All types:
- I have used inheritence in order to reduce duplication of code, so type1_npc is a child of npc, which is a child of monobehaviour (unity default)
- Each type calls a method that each npc requires, for example if a specific npc cannot hear, then the child should not be calling listen()
- Health (Health class)
- Ammo (Ammo class)
- Animators in the Animators folder
### Type 1:
- Random waypoints for NPC to follow (type1_npc.cs line 32)
- senses (NPC class, self explanatory methods)
- shooting, example in type1_npc.cs line 35 when the npc is chasing.

### Type 2:
- wander method for wandering in random places
- If NPC requires ammo or health the code in the npc class is called inside of type2_npc.cs to requireAmmoSearch(), line 104.
- NPC wanders if there is no health packs available

### Type 3:
- prioritizes following the player
- will only shoot if player is close enough to the npc (line 27 in type3_npc.cs)

### World:
- Health packs & ammo boxes respawn after 5 seconds (HealthPack and AmmoBox class' update method)
- Player needs to collect 10 health packs to win the game (Player.cs line 20)
- If the player is shot enough times they will die! (Player.cs line 29)
- Player can heal with health packs (HealthPack.cs line 45)
- When NPC and player collide the game is lost (OnTriggerEnter in Player.cs)
- Type 3 enemy features platforms, off-mesh links and obstacles with different travel cost. (all done through nav in unity)
- All NPCs use their own FSM
- There is a coloured sphere above every NPC to distinguish which is which
- There is a time limit of 3 minutes, if the player fails to collect 10 health packs the game is lost. (Player.cs line 29)
