using bravens.Managers;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace bravens.ObjectComponent.Components
{
    public class FinalBossGun : Component
    {
        private GameObjectManager GameObjectManager { get; }

        private readonly Transform transform;

        private float spiralDuration = 2f;
        private float spiralTimer = 0;
        public bool isSpiraling = false;
        private int bulletsPerSpiral = 64;
        private float spiralRadius = 200f;
        private float bulletSpeed = 300f;

        private int bulletsFired;

        private static int projectileCount = 0;

        public FinalBossGun(GameObject parent) : base(parent, nameof(FinalBossGun))
        {
            GameObjectManager = parent.Core.GameObjectManager;
            transform = parent.GetComponent<Transform>();
        }

        public override void Update(GameTime deltaTime)
        {
            if (!isSpiraling) return;

            float elapsed = (float)deltaTime.ElapsedGameTime.TotalSeconds;
            spiralTimer += elapsed;

            float progress = MathHelper.Clamp(spiralTimer / spiralDuration, 0f, 1f);
            int targetBulletsFired = (int)(bulletsPerSpiral * progress);

            while (bulletsFired < targetBulletsFired) 
            {
                FireSpiralProjectile(bulletsFired);
                bulletsFired++;
            }

            if (spiralTimer >= spiralDuration) 
            {
                isSpiraling = false;
                bulletsFired = 0;
            }
        }

        public void StartSpiralAttack() 
        {
            if (isSpiraling) return;

            spiralTimer = 0f;
            isSpiraling = true;
        }

        public void CreateAndFireBurstProjectiles()
        {
            Vector2 position = GetGameObject().GetComponent<Transform>().Position;

            GameObject projectile1 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
            GameObject projectile2 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
            GameObject projectile3 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
            GameObject projectile4 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
            GameObject projectile5 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
            GameObject projectile6 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
            GameObject projectile7 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
            GameObject projectile8 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
            GameObject projectile9 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
            GameObject projectile10 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
            GameObject projectile11 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
            GameObject projectile12 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
            GameObject projectile13 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
            GameObject projectile14 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
            GameObject projectile15 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
            GameObject projectile16 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");

            projectile1.AddComponent(() => new FinalBossProjectile(projectile1, new Vector2(0, 1)));
            projectile2.AddComponent(() => new FinalBossProjectile(projectile2, new Vector2(1, 0)));
            projectile3.AddComponent(() => new FinalBossProjectile(projectile3, new Vector2(0, -1)));
            projectile4.AddComponent(() => new FinalBossProjectile(projectile4, new Vector2(-1, 0)));
            projectile5.AddComponent(() => new FinalBossProjectile(projectile5, new Vector2(1, 1)));
            projectile6.AddComponent(() => new FinalBossProjectile(projectile6, new Vector2(1, -1)));
            projectile7.AddComponent(() => new FinalBossProjectile(projectile7, new Vector2(-1, 1)));
            projectile8.AddComponent(() => new FinalBossProjectile(projectile8, new Vector2(-1, -1)));

            projectile9.AddComponent(() => new FinalBossProjectile(projectile9, new Vector2(1, 0.5f)));
            projectile10.AddComponent(() => new FinalBossProjectile(projectile10, new Vector2(1, -0.5f)));
            projectile11.AddComponent(() => new FinalBossProjectile(projectile11, new Vector2(-1, 0.5f)));
            projectile12.AddComponent(() => new FinalBossProjectile(projectile12, new Vector2(-1, -0.5f)));
            projectile13.AddComponent(() => new FinalBossProjectile(projectile13, new Vector2(0.5f, 1)));
            projectile14.AddComponent(() => new FinalBossProjectile(projectile14, new Vector2(-0.5f, 1)));
            projectile15.AddComponent(() => new FinalBossProjectile(projectile15, new Vector2(0.5f, -1)));
            projectile16.AddComponent(() => new FinalBossProjectile(projectile16, new Vector2(-0.5f, -1)));

            List<GameObject> projectiles = new List<GameObject>();
            
            projectiles.Add(projectile1);
            projectiles.Add(projectile2);
            projectiles.Add(projectile3);
            projectiles.Add(projectile4);
            projectiles.Add(projectile5);
            projectiles.Add(projectile6);
            projectiles.Add(projectile7);
            projectiles.Add(projectile8);
            projectiles.Add(projectile9);
            projectiles.Add(projectile10);
            projectiles.Add(projectile11);
            projectiles.Add(projectile12);
            projectiles.Add(projectile13);
            projectiles.Add(projectile14);
            projectiles.Add(projectile15);
            projectiles.Add(projectile16);


            foreach (var projectile in projectiles) 
            {
                projectile.AddComponent<Collider>();
                projectile.GetComponent<Collider>().Tag = Enums.CollisionTag.EnemyProjectile;
                projectile.GetComponent<Transform>().Translate(position);
            }
        }

        public void CreateAndFireHeavyProjectile() 
        {
            Vector2 position = GetGameObject().GetComponent<Transform>().Position;

            GameObject projectile1 = GameObjectManager.Create($"FinalBossHeavyProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-large");
            GameObject projectile2 = GameObjectManager.Create($"FinalBossHeavyProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-large");
            GameObject projectile3 = GameObjectManager.Create($"FinalBossHeavyProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-large");
            GameObject projectile4 = GameObjectManager.Create($"FinalBossHeavyProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-large");
  

            List<GameObject> projectiles = new List<GameObject>();
            
            projectiles.Add(projectile1);
            projectiles.Add(projectile2);
            projectiles.Add(projectile3);
            projectiles.Add(projectile4);

            projectile1.AddComponent(() => new FinalBossHeavyProjectile(projectile1, new Vector2(0, 1)));
            projectile2.AddComponent(() => new FinalBossHeavyProjectile(projectile2, new Vector2(0, -1)));
            projectile3.AddComponent(() => new FinalBossHeavyProjectile(projectile3, new Vector2(1, 0)));
            projectile4.AddComponent(() => new FinalBossHeavyProjectile(projectile4, new Vector2(-1, 0)));

            foreach (var projectile in projectiles)
            {
                projectile.AddComponent<Collider>();
                projectile.GetComponent<Collider>().Tag = Enums.CollisionTag.EnemyProjectile;
                projectile.GetComponent<Transform>().Translate(position);
            }
        }

        public void CreateAndFireCircularBurstProjectiles(int projectilesToSpawn = 64, float radius = 5f) 
        {
            Vector2 centerPosition = GetGameObject().GetComponent<Transform>().Position;

            for (int i = 0; i < projectilesToSpawn; i++)
            {
                float angle = MathHelper.TwoPi * (i / (float)projectilesToSpawn);

                Vector2 spawnPos = centerPosition + new Vector2(
                    (float)Math.Cos(angle) * radius,
                    (float)Math.Sin(angle) * radius
                );

                Vector2 direction = Vector2.Normalize(spawnPos - centerPosition);

                GameObject projectile = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
                projectile.GetComponent<Transform>().SetPositionXY(spawnPos.X, spawnPos.Y);
                projectile.AddComponent(() => new FinalBossProjectile(projectile, direction));
                projectile.AddComponent<Collider>().Tag = Enums.CollisionTag.EnemyProjectile;
            }
        }
        
        private void FireSpiralProjectile(int bulletIndex) 
        {
            float angle = MathHelper.TwoPi * (bulletIndex / (float)bulletsPerSpiral * 3f);

            float radius = spiralRadius * (bulletIndex / (float)bulletsPerSpiral);

            Vector2 spawnPos = transform.Position + new Vector2((float)Math.Cos(angle) * radius, (float)Math.Sin(angle) * radius);

            GameObject player = GameObjectManager.FindGameObjectByName("Player");
            if (player != null) 
            {
                Vector2 playerPosition = player.GetComponent<Transform>().Position;
                Vector2 direction = Vector2.Normalize(playerPosition - spawnPos);

                GameObject projectile = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "finalBossProjectile-small");
                projectile.GetComponent<Transform>().SetPositionXY(spawnPos.X, spawnPos.Y);
                projectile.AddComponent(() => new FinalBossProjectile(projectile, direction));
                projectile.AddComponent<Collider>().Tag = Enums.CollisionTag.EnemyProjectile;
            }
        }

    }
}
