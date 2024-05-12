using System;
using System.Collections.Generic;
using Godot;

// [Serializable]
public class PlayerRaycastController
{
    public float skinWidth = 0.02f;

    float HorizontalRayCount = 4;

    float VerticalRayCount = 3;

    float HorizontalRaySpacing;

    float VerticalRaySpacing;

    public List<RayCast2D> HorizontalRays;

    public List<RayCast2D> VerticalRays;
    public Player Player;
    public CollisionShape2D CS;
    public Rect2 Rect;
    public Vector2 Center;
    public Vector2 Size;
    public Vector2 Extents;

    public Vector2 TR => Center + new Vector2(Extents.X, -Extents.Y);
    public Vector2 TL => Center + new Vector2(-Extents.X, -Extents.Y);
    public Vector2 BR => Center + new Vector2(Extents.X, Extents.Y);
    public Vector2 BL => Center + new Vector2(-Extents.X, Extents.Y);
    public Node2D RaycastGroup;

    public struct CollisionInfo{
        public bool Left, Right = false;

        public CollisionInfo(){
            Left = Right = false;
        }
        public void Reset(){
            Left = Right = false;
        }
    }

    public CollisionInfo Collisions;

    public PlayerRaycastController(Player player)
    {
        Player = player;
        CS = Player.CS;
        RaycastGroup = new Node2D() { Name = "RaycastGroup", Position = CS.Position };
        HorizontalRays = new List<RayCast2D>();
        VerticalRays = new List<RayCast2D>();
        UpdateRectData();
        CalculateRaySpacing();
        InitRays();
    }

    public PlayerRaycastController()
    {
    }

    public void UpdateRectData()
    {
        Rect = Player.CollisionBox.GetRect();
        Center = Rect.GetCenter() + Vector2.Up * (RaycastGroup.Position.Y - CS.Position.Y);
        Size = Rect.Size - skinWidth * Vector2.One;
        Extents = Size / 2;
    }

    public void Execute()
    {
        Collisions.Reset();
        UpdateRectData();
        CalculateRaySpacing();
        CheckHorizontalCollisions();
    }

    public void CalculateRaySpacing()
    {
        HorizontalRaySpacing = Size.Y / (HorizontalRayCount - 1);
        VerticalRaySpacing = Size.X / (VerticalRayCount - 1);
    }

    public void InitRays()
    {
        Player.AddChild(RaycastGroup);
        for (int i = 0; i < HorizontalRayCount; i++)
        {
            RayCast2D ray = new RayCast2D();
            ray.Name = "HorizontalRaycast." + i;
            ray.CollisionMask = Player.CollisionMask;
            ray.Position = TR + Vector2.Down * HorizontalRaySpacing * i;
            ray.TargetPosition = Vector2.Right * 1f;
            HorizontalRays.Add(ray);
            RaycastGroup.AddChild(ray);
        }

        for (int i = 0; i < VerticalRayCount; i++)
        {
            RayCast2D ray = new RayCast2D();
            ray.Name = "VerticalRaycast." + i;
            ray.CollisionMask = Player.CollisionMask;
            ray.Position = BL + Vector2.Right * VerticalRaySpacing * i;
            ray.TargetPosition = Vector2.Down * 1f;
            VerticalRays.Add(ray);
            RaycastGroup.AddChild(ray);
        }
    }

    public void CheckHorizontalCollisions()
    {
        float rayLength = 5.5f + Mathf.Abs(Player.velocity.X / Engine.MaxFps);

        for (int i = 0; i < HorizontalRays.Count; i++)
        {
            RayCast2D ray = HorizontalRays[i];
            ray.Position =  TR + Vector2.Down * HorizontalRaySpacing * i;
            ray.TargetPosition = rayLength * Vector2.Right;
            Vector2 collisionNormal = ray.GetCollisionNormal();
            if (ray.IsColliding() && (collisionNormal.X == 1f || collisionNormal.X == -1f))
            {
                Collisions.Left = Player.Facing == -1;
                Collisions.Right = Player.Facing == 1;
            }
        }
    }
}