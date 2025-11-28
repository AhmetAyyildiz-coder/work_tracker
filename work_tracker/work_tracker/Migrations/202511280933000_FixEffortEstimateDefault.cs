namespace work_tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixEffortEstimateDefault : DbMigration
    {
        public override void Up()
        {
            // EffortEstimate alanının varsayılan değerini kaldır
            // Bu alan null olabilmeli, kullanıcı tarafından manuel olarak girilmeli
            AlterColumn("dbo.WorkItems", "EffortEstimate", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            // Geri alma işlemi - orijinal duruma geri dön
            AlterColumn("dbo.WorkItems", "EffortEstimate", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}