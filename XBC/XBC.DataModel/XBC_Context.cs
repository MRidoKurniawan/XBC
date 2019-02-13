namespace XBC.DataModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class XBC_Context : DbContext
    {
        public XBC_Context()
            : base("name=XBC_Context")
        {
        }

        public virtual DbSet<t_assignment> t_assignment { get; set; }
        public virtual DbSet<t_audit_log> t_audit_log { get; set; }
        public virtual DbSet<t_batch> t_batch { get; set; }
        public virtual DbSet<t_batch_test> t_batch_test { get; set; }
        public virtual DbSet<t_biodata> t_biodata { get; set; }
        public virtual DbSet<t_bootcamp_test_type> t_bootcamp_test_type { get; set; }
        public virtual DbSet<t_bootcamp_type> t_bootcamp_type { get; set; }
        public virtual DbSet<t_category> t_category { get; set; }
        public virtual DbSet<t_clazz> t_clazz { get; set; }
        public virtual DbSet<t_document_test> t_document_test { get; set; }
        public virtual DbSet<t_document_test_detail> t_document_test_detail { get; set; }
        public virtual DbSet<t_feedback> t_feedback { get; set; }
        public virtual DbSet<t_global_parameter> t_global_parameter { get; set; }
        public virtual DbSet<t_idle_news> t_idle_news { get; set; }
        public virtual DbSet<t_menu> t_menu { get; set; }
        public virtual DbSet<t_menu_access> t_menu_access { get; set; }
        public virtual DbSet<t_monitoring> t_monitoring { get; set; }
        public virtual DbSet<t_office> t_office { get; set; }
        public virtual DbSet<t_question> t_question { get; set; }
        public virtual DbSet<t_role> t_role { get; set; }
        public virtual DbSet<t_room> t_room { get; set; }
        public virtual DbSet<t_technology> t_technology { get; set; }
        public virtual DbSet<t_technology_trainer> t_technology_trainer { get; set; }
        public virtual DbSet<t_test> t_test { get; set; }
        public virtual DbSet<t_test_type> t_test_type { get; set; }
        public virtual DbSet<t_testimony> t_testimony { get; set; }
        public virtual DbSet<t_trainer> t_trainer { get; set; }
        public virtual DbSet<t_user> t_user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<t_assignment>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<t_assignment>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<t_assignment>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<t_audit_log>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<t_audit_log>()
                .Property(e => e.json_insert)
                .IsUnicode(false);

            modelBuilder.Entity<t_audit_log>()
                .Property(e => e.json_before)
                .IsUnicode(false);

            modelBuilder.Entity<t_audit_log>()
                .Property(e => e.json_after)
                .IsUnicode(false);

            modelBuilder.Entity<t_audit_log>()
                .Property(e => e.json_delete)
                .IsUnicode(false);

            modelBuilder.Entity<t_batch>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<t_batch>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<t_batch>()
                .HasMany(e => e.t_batch_test)
                .WithRequired(e => e.t_batch)
                .HasForeignKey(e => e.batch_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_batch>()
                .HasMany(e => e.t_clazz)
                .WithRequired(e => e.t_batch)
                .HasForeignKey(e => e.batch_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_biodata>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<t_biodata>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<t_biodata>()
                .Property(e => e.last_education)
                .IsUnicode(false);

            modelBuilder.Entity<t_biodata>()
                .Property(e => e.graduation_year)
                .IsUnicode(false);

            modelBuilder.Entity<t_biodata>()
                .Property(e => e.educational_level)
                .IsUnicode(false);

            modelBuilder.Entity<t_biodata>()
                .Property(e => e.majors)
                .IsUnicode(false);

            modelBuilder.Entity<t_biodata>()
                .Property(e => e.gpa)
                .IsUnicode(false);

            modelBuilder.Entity<t_biodata>()
                .Property(e => e.du)
                .IsUnicode(false);

            modelBuilder.Entity<t_biodata>()
                .Property(e => e.tro)
                .IsUnicode(false);

            modelBuilder.Entity<t_biodata>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<t_biodata>()
                .Property(e => e.interviewer)
                .IsUnicode(false);

            modelBuilder.Entity<t_biodata>()
                .HasMany(e => e.t_assignment)
                .WithRequired(e => e.t_biodata)
                .HasForeignKey(e => e.biodata_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_biodata>()
                .HasMany(e => e.t_clazz)
                .WithRequired(e => e.t_biodata)
                .HasForeignKey(e => e.biodata_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_biodata>()
                .HasMany(e => e.t_monitoring)
                .WithRequired(e => e.t_biodata)
                .HasForeignKey(e => e.biodata_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_bootcamp_test_type>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<t_bootcamp_test_type>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<t_bootcamp_type>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<t_bootcamp_type>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<t_bootcamp_type>()
                .HasMany(e => e.t_batch)
                .WithOptional(e => e.t_bootcamp_type)
                .HasForeignKey(e => e.bootcamp_type_id);

            modelBuilder.Entity<t_category>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<t_category>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<t_category>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<t_category>()
                .HasMany(e => e.t_idle_news)
                .WithRequired(e => e.t_category)
                .HasForeignKey(e => e.category_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_document_test>()
                .Property(e => e.version)
                .HasPrecision(5, 0);

            modelBuilder.Entity<t_document_test>()
                .Property(e => e.token)
                .IsUnicode(false);

            modelBuilder.Entity<t_document_test>()
                .HasMany(e => e.t_document_test_detail)
                .WithRequired(e => e.t_document_test)
                .HasForeignKey(e => e.document_test_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_document_test>()
                .HasMany(e => e.t_feedback)
                .WithRequired(e => e.t_document_test)
                .HasForeignKey(e => e.document_test_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_feedback>()
                .Property(e => e.json_feedback)
                .IsUnicode(false);

            modelBuilder.Entity<t_global_parameter>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<t_global_parameter>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<t_global_parameter>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<t_idle_news>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<t_idle_news>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<t_menu>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<t_menu>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<t_menu>()
                .Property(e => e.descriptoin)
                .IsUnicode(false);

            modelBuilder.Entity<t_menu>()
                .Property(e => e.image_url)
                .IsUnicode(false);

            modelBuilder.Entity<t_menu>()
                .Property(e => e.menu_url)
                .IsUnicode(false);

            modelBuilder.Entity<t_menu>()
                .HasMany(e => e.t_menu_access)
                .WithRequired(e => e.t_menu)
                .HasForeignKey(e => e.menu_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_monitoring>()
                .Property(e => e.last_project)
                .IsUnicode(false);

            modelBuilder.Entity<t_monitoring>()
                .Property(e => e.idle_reason)
                .IsUnicode(false);

            modelBuilder.Entity<t_monitoring>()
                .Property(e => e.placement_at)
                .IsUnicode(false);

            modelBuilder.Entity<t_monitoring>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<t_office>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<t_office>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<t_office>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<t_office>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<t_office>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<t_office>()
                .HasMany(e => e.t_room)
                .WithRequired(e => e.t_office)
                .HasForeignKey(e => e.office_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_question>()
                .Property(e => e.question)
                .IsUnicode(false);

            modelBuilder.Entity<t_question>()
                .Property(e => e.question_type)
                .IsUnicode(false);

            modelBuilder.Entity<t_question>()
                .Property(e => e.option_a)
                .IsUnicode(false);

            modelBuilder.Entity<t_question>()
                .Property(e => e.option_b)
                .IsUnicode(false);

            modelBuilder.Entity<t_question>()
                .Property(e => e.option_c)
                .IsUnicode(false);

            modelBuilder.Entity<t_question>()
                .Property(e => e.option_d)
                .IsUnicode(false);

            modelBuilder.Entity<t_question>()
                .Property(e => e.option_e)
                .IsUnicode(false);

            modelBuilder.Entity<t_question>()
                .Property(e => e.image_url)
                .IsUnicode(false);

            modelBuilder.Entity<t_question>()
                .Property(e => e.image_a)
                .IsUnicode(false);

            modelBuilder.Entity<t_question>()
                .Property(e => e.image_b)
                .IsUnicode(false);

            modelBuilder.Entity<t_question>()
                .Property(e => e.image_c)
                .IsUnicode(false);

            modelBuilder.Entity<t_question>()
                .Property(e => e.image_d)
                .IsUnicode(false);

            modelBuilder.Entity<t_question>()
                .Property(e => e.image_e)
                .IsUnicode(false);

            modelBuilder.Entity<t_question>()
                .HasMany(e => e.t_document_test_detail)
                .WithRequired(e => e.t_question)
                .HasForeignKey(e => e.question_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_role>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<t_role>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<t_role>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<t_role>()
                .HasMany(e => e.t_menu_access)
                .WithRequired(e => e.t_role)
                .HasForeignKey(e => e.role_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_role>()
                .HasMany(e => e.t_user)
                .WithRequired(e => e.t_role)
                .HasForeignKey(e => e.role_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_room>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<t_room>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<t_room>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<t_room>()
                .HasMany(e => e.t_batch)
                .WithOptional(e => e.t_room)
                .HasForeignKey(e => e.room_id);

            modelBuilder.Entity<t_technology>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<t_technology>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<t_technology>()
                .HasMany(e => e.t_batch)
                .WithRequired(e => e.t_technology)
                .HasForeignKey(e => e.technology_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_technology>()
                .HasMany(e => e.t_technology_trainer)
                .WithRequired(e => e.t_technology)
                .HasForeignKey(e => e.technology_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_test>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<t_test>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<t_test>()
                .HasMany(e => e.t_batch_test)
                .WithRequired(e => e.t_test)
                .HasForeignKey(e => e.test_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_test>()
                .HasMany(e => e.t_document_test)
                .WithRequired(e => e.t_test)
                .HasForeignKey(e => e.test_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_test>()
                .HasMany(e => e.t_feedback)
                .WithRequired(e => e.t_test)
                .HasForeignKey(e => e.test_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_test_type>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<t_test_type>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<t_test_type>()
                .HasMany(e => e.t_document_test)
                .WithRequired(e => e.t_test_type)
                .HasForeignKey(e => e.test_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_testimony>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<t_testimony>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<t_trainer>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<t_trainer>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<t_trainer>()
                .HasMany(e => e.t_batch)
                .WithRequired(e => e.t_trainer)
                .HasForeignKey(e => e.trainer_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_trainer>()
                .HasMany(e => e.t_technology_trainer)
                .WithRequired(e => e.t_trainer)
                .HasForeignKey(e => e.trainer_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<t_user>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<t_user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<t_user>()
                .Property(e => e.email)
                .IsUnicode(false);
        }
    }
}
