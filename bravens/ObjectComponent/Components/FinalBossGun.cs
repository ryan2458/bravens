using bravens.Managers;
using bravens.ObjectComponent.Objects;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace bravens.ObjectComponent.Components
{
    public class FinalBossGun : Component
    {
        private GameObjectManager GameObjectManager { get; }

        private static int projectileCount = 0;

        public FinalBossGun(GameObject parent) : base(parent, nameof(FinalBossGun))
        {
            GameObjectManager = parent.Core.GameObjectManager;
        }

        public override void Update(GameTime deltaTime)
        {
            
        }

        public void CreateAndFireBurstProjectiles()
        {
            Vector2 position = GetGameObject().GetComponent<Transform>().Position;

            GameObject projectile1 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile2 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile3 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile4 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile5 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile6 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile7 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile8 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile9 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile10 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile11 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile12 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile13 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile14 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile15 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");
            GameObject projectile16 = GameObjectManager.Create($"FinalBossProjectile{projectileCount++}", GetGameObject(), "bossProjectile");

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
    }
}
