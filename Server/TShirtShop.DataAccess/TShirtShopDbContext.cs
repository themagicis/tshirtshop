using Microsoft.EntityFrameworkCore;
using TShirtShop.DataAccess.Models;

namespace TShirtShop.DataAccess
{
    public class TShirtShopDbContext : DbContext
    {
        public TShirtShopDbContext(DbContextOptions<TShirtShopDbContext> options) 
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Attributte>(entity =>
            {
                entity.ToTable("attribute");

                entity.Property(e => e.AttributeId).HasColumnName("attribute_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AttributeValue>(entity =>
            {
                entity.ToTable("attribute_value");

                entity.HasIndex(e => e.AttributeId)
                    .HasName("idx_attribute_value_attribute_id");

                entity.Property(e => e.AttributeValueId).HasColumnName("attribute_value_id");

                entity.Property(e => e.AttributeId).HasColumnName("attribute_id");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("audit");

                entity.HasIndex(e => e.OrderId)
                    .HasName("idx_audit_order_id");

                entity.Property(e => e.AuditId).HasColumnName("audit_id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnName("message")
                    .HasColumnType("text");

                entity.Property(e => e.OrderId).HasColumnName("order_id");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("idx_category_department_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.Email)
                    .HasName("idx_customer_email")
                    .IsUnique();

                entity.HasIndex(e => e.ShippingRegionId)
                    .HasName("idx_customer_shipping_region_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Address1)
                    .HasColumnName("address_1")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasColumnName("address_2")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreditCard)
                    .HasColumnName("credit_card")
                    .HasColumnType("text");

                entity.Property(e => e.DayPhone)
                    .HasColumnName("day_phone")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EvePhone)
                    .HasColumnName("eve_phone")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MobPhone)
                    .HasColumnName("mob_phone")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasColumnName("postal_code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Region)
                    .HasColumnName("region")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingRegionId)
                    .HasColumnName("shipping_region_id")
                    .HasDefaultValueSql("('1')");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__order_de__52020FDDFD4EC0A8");

                entity.ToTable("order_detail");

                entity.HasIndex(e => e.OrderId)
                    .HasName("idx_order_detail_order_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Attributes)
                    .IsRequired()
                    .HasColumnName("attributes")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UnitCost)
                    .HasColumnName("unit_cost")
                    .HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__orders__465962290F8F0B08");

                entity.ToTable("orders");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("idx_orders_customer_id");

                entity.HasIndex(e => e.ShippingId)
                    .HasName("idx_orders_shipping_id");

                entity.HasIndex(e => e.TaxId)
                    .HasName("idx_orders_tax_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.AuthCode)
                    .HasColumnName("auth_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Reference)
                    .HasColumnName("reference")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShippedOn)
                    .HasColumnName("shipped_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.ShippingId).HasColumnName("shipping_id");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.TaxId).HasColumnName("tax_id");

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("total_amount")
                    .HasColumnType("decimal(10, 2)")
                    .HasDefaultValueSql("('0.00')");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountedPrice)
                    .HasColumnName("discounted_price")
                    .HasColumnType("decimal(10, 2)")
                    .HasDefaultValueSql("('0.00')");

                entity.Property(e => e.Display)
                    .HasColumnName("display")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Image2)
                    .HasColumnName("image_2")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Thumbnail)
                    .HasColumnName("thumbnail")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.AttributeValueId })
                    .HasName("PK__product___3B8307DABF7871A8");

                entity.ToTable("product_attribute");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.AttributeValueId).HasColumnName("attribute_value_id");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CategoryId })
                    .HasName("PK__product___1A56936E83545F97");

                entity.ToTable("product_category");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("review");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("idx_review_customer_id");

                entity.HasIndex(e => e.ProductId)
                    .HasName("idx_review_product_id");

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Review1)
                    .IsRequired()
                    .HasColumnName("review")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.ToTable("shipping");

                entity.HasIndex(e => e.ShippingRegionId)
                    .HasName("idx_shipping_shipping_region_id");

                entity.Property(e => e.ShippingId).HasColumnName("shipping_id");

                entity.Property(e => e.ShippingCost)
                    .HasColumnName("shipping_cost")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.ShippingRegionId).HasColumnName("shipping_region_id");

                entity.Property(e => e.ShippingType)
                    .IsRequired()
                    .HasColumnName("shipping_type")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShippingRegion>(entity =>
            {
                entity.ToTable("shipping_region");

                entity.Property(e => e.ShippingRegionId).HasColumnName("shipping_region_id");

                entity.Property(e => e.ShippingRegion1)
                    .IsRequired()
                    .HasColumnName("shipping_region")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__shopping__52020FDD095C36F9");

                entity.ToTable("shopping_cart");

                entity.HasIndex(e => e.CartId)
                    .HasName("idx_shopping_cart_cart_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.AddedOn)
                    .HasColumnName("added_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Attributes)
                    .IsRequired()
                    .HasColumnName("attributes")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.BuyNow)
                    .IsRequired()
                    .HasColumnName("buy_now")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CartId)
                    .IsRequired()
                    .HasColumnName("cart_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<Tax>(entity =>
            {
                entity.ToTable("tax");

                entity.Property(e => e.TaxId).HasColumnName("tax_id");

                entity.Property(e => e.TaxPercentage)
                    .HasColumnName("tax_percentage")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.TaxType)
                    .IsRequired()
                    .HasColumnName("tax_type")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
