using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;

namespace FlamethrowerScript
{
  public class Class1 : Script
  {
    public bool FlamethrowerOn = true;
    public List<int> Fire__ids = new List<int>();
    public List<int> PTFX__ids = new List<int>();
    public List<flame> Flames = new List<flame>();
    public int PedsKilled;
    public bool ModOn;
    public ScriptSettings Config;

    public Class1()
    {
      Function.Call((Hash) -5184338789570016586L, new InputArgument[1]
      {
        InputArgument.op_Implicit("core")
      });
      Function.Call((Hash) 7798175403732277905L, new InputArgument[1]
      {
        InputArgument.op_Implicit("core")
      });
      Function.Call((Hash) -5184338789570016586L, new InputArgument[1]
      {
        InputArgument.op_Implicit("core")
      });
      Function.Call((Hash) 7798175403732277905L, new InputArgument[1]
      {
        InputArgument.op_Implicit("core")
      });
      Function.Call((Hash) -5184338789570016586L, new InputArgument[1]
      {
        InputArgument.op_Implicit("core")
      });
      Function.Call((Hash) 7798175403732277905L, new InputArgument[1]
      {
        InputArgument.op_Implicit("core")
      });
      Function.Call((Hash) -5184338789570016586L, new InputArgument[1]
      {
        InputArgument.op_Implicit("core")
      });
      Function.Call((Hash) 7798175403732277905L, new InputArgument[1]
      {
        InputArgument.op_Implicit("core")
      });
      this.LoadIniFile("scripts\\FlamethrowerScript.ini");
      this.Tick += new EventHandler(this.OnTick);
      this.Aborted += new EventHandler(this.OnShutdown);
      UI.Notify("~r~ Flamethrower Script ~w~ Loaded, created by ~g~HKH191~w~");
    }

    public void LoadIniFile(string iniName)
    {
      try
      {
        this.Config = ScriptSettings.Load(iniName);
        this.ModOn = (bool) this.Config.GetValue<bool>("Config", "ModOn", (M0) (this.ModOn ? 1 : 0));
      }
      catch (Exception ex)
      {
        UI.Notify("~r~Error~w~: Config.ini Failed To Load.");
      }
    }

    private void SetColor(int particle, float r, float g, float b, bool p1) => Function.Call((Hash) 9191676997121112123L, new InputArgument[5]
    {
      InputArgument.op_Implicit(particle),
      InputArgument.op_Implicit(r),
      InputArgument.op_Implicit(g),
      InputArgument.op_Implicit(b),
      InputArgument.op_Implicit(p1)
    });

    private void SetRange(int handle, float range) => Function.Call((Hash) -2544088794899434175L, new InputArgument[2]
    {
      InputArgument.op_Implicit(handle),
      InputArgument.op_Implicit(range)
    });

    private int GetBoneByName(Entity entity, string name) => (int) Function.Call<int>((Hash) -328455959687549766L, new InputArgument[2]
    {
      InputArgument.op_Implicit(entity),
      InputArgument.op_Implicit(name)
    });

    private void OnTick(object sender, EventArgs e)
    {
      if (!this.ModOn)
        return;
      if ((uint) Game.Player.WantedLevel > 0U)
      {
        foreach (Ped nearbyPed in World.GetNearbyPeds(((Entity) Game.Player.Character).Position, 20f))
        {
          if (((Entity) nearbyPed).IsAlive && !((Entity) nearbyPed).IsOnFire)
          {
            Model model = ((Entity) nearbyPed).Model;
            if (((Model) ref model).IsPed && !((Entity) nearbyPed).IsOnFire)
            {
              ++this.PedsKilled;
              break;
            }
            if (this.PedsKilled == 20 && Game.Player.WantedLevel == 2)
              Game.Player.WantedLevel = 3;
            if (this.PedsKilled == 30 && Game.Player.WantedLevel == 3)
              Game.Player.WantedLevel = 4;
            if (this.PedsKilled == 40 && Game.Player.WantedLevel == 4)
              Game.Player.WantedLevel = 5;
          }
        }
      }
      if (Game.Player.WantedLevel == 0 && (uint) this.PedsKilled > 0U)
        this.PedsKilled = 0;
      if (this.FlamethrowerOn)
      {
        try
        {
          if (this.Flames.Count > 0)
          {
            foreach (flame flame in this.Flames)
            {
              if (flame.Type == 2)
              {
                if (flame.LifeTime != 5)
                  ++flame.LifeTime;
                if (flame.LifeTime >= 5)
                  Function.Call((Hash) 9220355218917582655L, new InputArgument[1]
                  {
                    InputArgument.op_Implicit(flame.X1)
                  });
              }
              if (flame.Type == 1)
              {
                if (flame.LifeTime != 5)
                  ++flame.LifeTime;
                if (flame.LifeTime >= 5)
                {
                  if ((bool) Function.Call<bool>((Hash) 8408201869211353243L, new InputArgument[1]
                  {
                    InputArgument.op_Implicit(flame.ID)
                  }))
                    Function.Call((Hash) -8109406742613235306L, new InputArgument[2]
                    {
                      InputArgument.op_Implicit(flame.ID),
                      InputArgument.op_Implicit(false)
                    });
                }
              }
            }
          }
        }
        catch
        {
        }
        if (Game.Player.Character.Weapons.Current != null)
        {
          if (Game.Player.Character.Weapons.Current.Hash != -1238556825)
            ((Entity) Game.Player.Character).IsFireProof = false;
          if (Game.Player.Character.Weapons.Current.Hash == -1238556825)
          {
            ((Entity) Game.Player.Character).IsFireProof = true;
            ((Entity) Game.Player.Character.Weapons.CurrentWeaponObject).GetOffsetInWorldCoords(new Vector3(1f, 0.0f, 0.0f));
            if (Game.IsControlPressed(2, (Control) 24) && Game.Player.Character.Weapons.Current.Ammo >= 0)
            {
              --Game.Player.Character.Weapons.Current.Ammo;
              Function.Call((Hash) 6804024751275371192L, new InputArgument[2]
              {
                InputArgument.op_Implicit(Game.Player),
                InputArgument.op_Implicit(true)
              });
              ((Entity) Game.Player.Character).IsFireProof = true;
              Function.Call((Hash) -5184338789570016586L, new InputArgument[1]
              {
                InputArgument.op_Implicit("core")
              });
              Function.Call((Hash) 7798175403732277905L, new InputArgument[1]
              {
                InputArgument.op_Implicit("core")
              });
              Function.Call((Hash) -5184338789570016586L, new InputArgument[1]
              {
                InputArgument.op_Implicit("core")
              });
              Function.Call((Hash) 7798175403732277905L, new InputArgument[1]
              {
                InputArgument.op_Implicit("core")
              });
              int num1 = (int) Function.Call<int>((Hash) 1937722214304277783L, new InputArgument[12]
              {
                InputArgument.op_Implicit("ent_sht_flame"),
                InputArgument.op_Implicit(Game.Player.Character.Weapons.CurrentWeaponObject),
                InputArgument.op_Implicit(1f),
                InputArgument.op_Implicit(0.0f),
                InputArgument.op_Implicit(0.0f),
                InputArgument.op_Implicit(0.0f),
                InputArgument.op_Implicit(90f),
                InputArgument.op_Implicit(0.0f),
                InputArgument.op_Implicit(6f),
                InputArgument.op_Implicit(false),
                InputArgument.op_Implicit(false),
                InputArgument.op_Implicit(false)
              });
              this.PTFX__ids.Add(num1);
              this.Flames.Add(new flame(1, num1, 0));
              this.SetColor(num1, (float) byte.MaxValue, 0.0f, (float) byte.MaxValue, true);
              List<Vector3> vector3List = new List<Vector3>();
              Vector3 offsetInWorldCoords1 = ((Entity) Game.Player.Character.Weapons.CurrentWeaponObject).GetOffsetInWorldCoords(new Vector3(5f, 0.0f, 0.0f));
              vector3List.Add(offsetInWorldCoords1);
              Vector3 offsetInWorldCoords2 = ((Entity) Game.Player.Character.Weapons.CurrentWeaponObject).GetOffsetInWorldCoords(new Vector3(6f, 0.0f, 0.0f));
              vector3List.Add(offsetInWorldCoords2);
              Vector3 offsetInWorldCoords3 = ((Entity) Game.Player.Character.Weapons.CurrentWeaponObject).GetOffsetInWorldCoords(new Vector3(7f, 0.0f, 0.0f));
              vector3List.Add(offsetInWorldCoords3);
              Vector3 offsetInWorldCoords4 = ((Entity) Game.Player.Character.Weapons.CurrentWeaponObject).GetOffsetInWorldCoords(new Vector3(8f, 0.0f, 0.0f));
              vector3List.Add(offsetInWorldCoords4);
              foreach (Vector3 vector3 in vector3List)
              {
                Random random = new Random();
                foreach (Entity nearbyEntity in World.GetNearbyEntities(vector3, 3f))
                {
                  if ((double) World.GetDistance(nearbyEntity.Position, ((Entity) Game.Player.Character).Position) > 3.0 && nearbyEntity.IsAlive)
                  {
                    Model model = nearbyEntity.Model;
                    int num2;
                    if (!((Model) ref model).IsVehicle)
                    {
                      model = nearbyEntity.Model;
                      num2 = ((Model) ref model).IsPed ? 1 : 0;
                    }
                    else
                      num2 = 1;
                    if (num2 != 0)
                      this.Flames.Add(new flame((int) Function.Call<int>((Hash) 7747142977873524872L, new InputArgument[5]
                      {
                        InputArgument.op_Implicit((float) nearbyEntity.Position.X),
                        InputArgument.op_Implicit((float) nearbyEntity.Position.Y),
                        InputArgument.op_Implicit((float) nearbyEntity.Position.Z),
                        InputArgument.op_Implicit(3),
                        InputArgument.op_Implicit(true)
                      }), 0));
                  }
                }
              }
              Random random1 = new Random();
              Vector3 vector3_1 = ((Vector3) ref offsetInWorldCoords1).Around((float) random1.Next(1, 5));
              // ISSUE: explicit constructor call
              ((Vector3) ref vector3_1).\u002Ector((float) vector3_1.X, (float) vector3_1.Y, World.GetGroundHeight(vector3_1) + 0.25f);
              this.Flames.Add(new flame((int) Function.Call<int>((Hash) 7747142977873524872L, new InputArgument[5]
              {
                InputArgument.op_Implicit((float) vector3_1.X),
                InputArgument.op_Implicit((float) vector3_1.Y),
                InputArgument.op_Implicit((float) vector3_1.Z),
                InputArgument.op_Implicit(3),
                InputArgument.op_Implicit(true)
              }), 0));
            }
            if (!Game.IsControlJustPressed(2, (Control) 24))
            {
              Function.Call((Hash) 6804024751275371192L, new InputArgument[2]
              {
                InputArgument.op_Implicit(Game.Player),
                InputArgument.op_Implicit(false)
              });
              try
              {
                if (this.Flames.Count > 0)
                {
                  foreach (flame flame in this.Flames)
                  {
                    if ((bool) Function.Call<bool>((Hash) 8408201869211353243L, new InputArgument[1]
                    {
                      InputArgument.op_Implicit(flame.ID)
                    }))
                      Function.Call((Hash) -8109406742613235306L, new InputArgument[2]
                      {
                        InputArgument.op_Implicit(flame.ID),
                        InputArgument.op_Implicit(false)
                      });
                    this.Flames.Remove(flame);
                  }
                }
              }
              catch
              {
              }
            }
          }
        }
      }
    }

    private void OnKeyDown()
    {
    }

    public static string LoadDict(string dict)
    {
      while (true)
      {
        if (Function.Call<bool>((Hash) -3444786327252301684L, new InputArgument[1]
        {
          InputArgument.op_Implicit(dict)
        }) == 0)
        {
          Function.Call((Hash) -3189321952077349130L, new InputArgument[1]
          {
            InputArgument.op_Implicit(dict)
          });
          Script.Yield();
        }
        else
          break;
      }
      return dict;
    }

    private void OnShutdown(object sender, EventArgs e)
    {
      if (false)
        return;
      foreach (flame flame in this.Flames)
      {
        if (flame.Type == 2)
          Function.Call((Hash) 9220355218917582655L, new InputArgument[1]
          {
            InputArgument.op_Implicit(flame.X1)
          });
        if (flame.Type == 1)
        {
          if ((bool) Function.Call<bool>((Hash) 8408201869211353243L, new InputArgument[1]
          {
            InputArgument.op_Implicit(flame.ID)
          }))
            Function.Call((Hash) -8109406742613235306L, new InputArgument[2]
            {
              InputArgument.op_Implicit(flame.ID),
              InputArgument.op_Implicit(false)
            });
        }
      }
    }
  }
}
