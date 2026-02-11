using TshirtMaker.Models.Core;
using TshirtMaker.Models.Social;
using TshirtMaker.Models.Enums;
using TshirtMaker.Utility;

namespace TshirtMaker.Tests
{
    public static class TestUsers
    {
        public static List<User> Users { get; set; } = new();
        public static List<Design> Designs { get; set; } = new();
        public static List<Post> Posts { get; set; } = new();
        public static List<Like> Likes { get; set; } = new();
        public static List<Comment> Comments { get; set; } = new();
        public static List<Bookmark> Bookmarks { get; set; } = new();
        public static List<Follower> Followers { get; set; } = new();
        public static List<Notification> Notifications { get; set; } = new();

        static TestUsers()
        {
            InitializeTestData();
        }

        private static void InitializeTestData()
        {
            // ==================== USER 1: CyberArtist ====================
            var user1 = new User
            {
                Username = "CyberArtist",
                Email = "cyber@test.com",
                PasswordHash = "hashed_password_123",
                Bio = "Cyberpunk & futuristic design enthusiast. Creating neon dreams on fabric.",
                AvatarUrl = "https://api.dicebear.com/7.x/avataaars/svg?seed=CyberArtist",
                CoverImageUrl = "https://images.unsplash.com/photo-1550745165-9bc0b252726f?w=1200&h=400&fit=crop",
                Location = "Tokyo, Japan",
                WebsiteUrl = "https://cyberartist.design",
                FollowingCount = 1,
                LastLoginAt = DateTime.UtcNow.AddHours(-2),
                CreatedAt = DateTime.UtcNow.AddMonths(-6)
            };

            // ==================== USER 2: MinimalMaven ====================
            var user2 = new User
            {
                Username = "MinimalMaven",
                Email = "minimal@test.com",
                PasswordHash = "hashed_password_456",
                Bio = "Less is more. Creating clean, minimal designs that speak volumes.",
                AvatarUrl = "https://api.dicebear.com/7.x/avataaars/svg?seed=MinimalMaven",
                CoverImageUrl = "https://images.unsplash.com/photo-1618005182384-a83a8bd57fbe?w=1200&h=400&fit=crop",
                Location = "Copenhagen, Denmark",
                WebsiteUrl = "https://minimalmaven.art",
                FollowingCount = 2,
                LastLoginAt = DateTime.UtcNow.AddHours(-1),
                CreatedAt = DateTime.UtcNow.AddMonths(-4)
            };

            // ==================== USER 3: VintageVibe ====================
            var user3 = new User
            {
                Username = "VintageVibe",
                Email = "vintage@test.com",
                PasswordHash = "hashed_password_789",
                Bio = "Bringing back the classics. Retro designs with a modern twist 🎨",
                AvatarUrl = "https://api.dicebear.com/7.x/avataaars/svg?seed=VintageVibe",
                CoverImageUrl = "https://images.unsplash.com/photo-1576566588028-4147f3842f27?w=1200&h=400&fit=crop",
                Location = "Austin, Texas",
                FollowingCount = 2,
                LastLoginAt = DateTime.UtcNow.AddMinutes(-30),
                CreatedAt = DateTime.UtcNow.AddMonths(-3)
            };

            // ==================== DESIGNS ====================

            // User 1 Designs
            var design1_1 = new Design
            {
                User = user1,
                UserId = user1.Id,
                Title = "Neon Tiger Circuit",
                Prompt = "Cyberpunk tiger with glowing neon circuits and holographic effects",
                NegativePrompt = "blur, low quality, watermark",
                StylePreset = StylePresetType.Cyberpunk,
                Resolution = Resolution.FourK,
                ClothingType = ClothingType.TShirt,
                Color = "#000000",
                Size = ClothingSize.L,
                Material = Material.HeavyCotton,
                PrintPosition = PrintPosition.Front,
                FinalImageUrl = "https://images.unsplash.com/photo-1618354691373-d851c5c3a990?w=800&h=800&fit=crop",
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            };

            var design1_2 = new Design
            {
                User = user1,
                UserId = user1.Id,
                Title = "Digital Samurai",
                Prompt = "Futuristic samurai warrior with neon katana in cyberpunk city",
                StylePreset = StylePresetType.Anime,
                Resolution = Resolution.TwoK,
                ClothingType = ClothingType.Hoodie,
                Color = "#191970",
                Size = ClothingSize.XL,
                Material = Material.Fleece,
                PrintPosition = PrintPosition.Back,
                FinalImageUrl = "https://images.unsplash.com/photo-1620799140408-edc6dcb6d633?w=800&h=800&fit=crop",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            };

            var design1_3 = new Design
            {
                User = user1,
                UserId = user1.Id,
                Title = "Glitch Matrix",
                Prompt = "Abstract glitch art with matrix code and digital corruption",
                StylePreset = StylePresetType.Abstract,
                Resolution = Resolution.FourK,
                ClothingType = ClothingType.TShirt,
                Color = "#00FF00",
                Size = ClothingSize.M,
                Material = Material.CottonPolyesterBlend,
                PrintPosition = PrintPosition.Front,
                FinalImageUrl = "https://images.unsplash.com/photo-1618221195710-dd6b41faaea6?w=800&h=800&fit=crop",
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            };

            // User 2 Designs
            var design2_1 = new Design
            {
                User = user2,
                UserId = user2.Id,
                Title = "Mountain Line",
                Prompt = "Single continuous line drawing of mountain landscape minimalist style",
                StylePreset = StylePresetType.Minimalism,
                Resolution = Resolution.Standard,
                ClothingType = ClothingType.Tank,
                Color = "#FFFFFF",
                Size = ClothingSize.S,
                Material = Material.HeavyCotton,
                PrintPosition = PrintPosition.Front,
                FinalImageUrl = "https://images.unsplash.com/photo-1523381210434-271e8be1f52b?w=800&h=800&fit=crop",
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            };

            var design2_2 = new Design
            {
                User = user2,
                UserId = user2.Id,
                Title = "Geometric Balance",
                Prompt = "Abstract geometric shapes with perfect balance and negative space",
                StylePreset = StylePresetType.Minimalism,
                Resolution = Resolution.TwoK,
                ClothingType = ClothingType.LongSleeve,
                Color = "#F5F5DC",
                Size = ClothingSize.M,
                Material = Material.Polyester,
                PrintPosition = PrintPosition.Front,
                FinalImageUrl = "https://images.unsplash.com/photo-1503341504253-dff4815485f1?w=800&h=800&fit=crop",
                CreatedAt = DateTime.UtcNow.AddDays(-12)
            };

            var design2_3 = new Design
            {
                User = user2,
                UserId = user2.Id,
                Title = "Circle Study",
                Prompt = "Minimalist concentric circles in perfect harmony",
                StylePreset = StylePresetType.Minimalism,
                Resolution = Resolution.Standard,
                ClothingType = ClothingType.Tank,
                Color = "#000000",
                Size = ClothingSize.M,
                Material = Material.CottonPolyesterBlend,
                PrintPosition = PrintPosition.LeftChest,
                FinalImageUrl = "https://images.unsplash.com/photo-1581655353564-df123a1eb820?w=800&h=800&fit=crop",
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            };

            // User 3 Designs
            var design3_1 = new Design
            {
                User = user3,
                UserId = user3.Id,
                Title = "Retro Sunset Wave",
                Prompt = "Vintage 80s sunset with palm trees and retro wave aesthetic",
                StylePreset = StylePresetType.Vintage,
                Resolution = Resolution.FourK,
                ClothingType = ClothingType.Jacket,
                Color = "#FF8C00",
                Size = ClothingSize.L,
                Material = Material.Polyester,
                PrintPosition = PrintPosition.Back,
                FinalImageUrl = "https://images.unsplash.com/photo-1576566588028-4147f3842f27?w=800&h=800&fit=crop",
                CreatedAt = DateTime.UtcNow.AddDays(-8)
            };

            var design3_2 = new Design
            {
                User = user3,
                UserId = user3.Id,
                Title = "Classic Badge",
                Prompt = "Vintage badge logo with retro typography and ornamental details",
                StylePreset = StylePresetType.Vintage,
                Resolution = Resolution.Standard,
                ClothingType = ClothingType.Hat,
                Color = "#8B4513",
                Size = ClothingSize.XXL,
                Material = Material.CottonPolyesterBlend,
                PrintPosition = PrintPosition.Front,
                FinalImageUrl = "https://images.unsplash.com/photo-1521369909029-2afed882baee?w=800&h=800&fit=crop",
                CreatedAt = DateTime.UtcNow.AddDays(-14)
            };

            var design3_3 = new Design
            {
                User = user3,
                UserId = user3.Id,
                Title = "Old School Vibes",
                Prompt = "Retro cassette tape with vintage colors and nostalgic feel",
                StylePreset = StylePresetType.Vintage,
                Resolution = Resolution.TwoK,
                ClothingType = ClothingType.Sweat,
                Color = "#F5DEB3",
                Size = ClothingSize.L,
                Material = Material.Fleece,
                PrintPosition = PrintPosition.Front,
                FinalImageUrl = "https://images.unsplash.com/photo-1550745165-9bc0b252726f?w=800&h=800&fit=crop",
                CreatedAt = DateTime.UtcNow.AddDays(-4)
            };

            // ==================== POSTS ====================

            // User 1 Posts
            var post1_1 = new Post
            {
                Poster = user1,
                PosterId = user1.Id,
                Design = design1_1,
                DesignId = design1_1.Id,
                Description = "My latest cyberpunk creation! What do you think? 🐯⚡",

                RemixCount = 2,
                AllowRemix = true,
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            };

            var post1_2 = new Post
            {
                Poster = user1,
                PosterId = user1.Id,
                Design = design1_2,
                DesignId = design1_2.Id,
                Description = "Samurai meets cyberpunk. Print on the back for maximum impact! 🗡️",
                RemixCount = 1,
                AllowRemix = true,
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            };

            var post1_3 = new Post
            {
                Poster = user1,
                PosterId = user1.Id,
                Design = design1_3,
                DesignId = design1_3.Id,
                Description = "Glitch in the matrix 💚 Love how this turned out on green!",

                RemixCount = 0,
                AllowRemix = true,
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            };

            // User 2 Posts
            var post2_1 = new Post
            {
                Poster = user2,
                PosterId = user2.Id,
                Design = design2_1,
                DesignId = design2_1.Id,
                Description = "Sometimes one line is all you need 🏔️",

                RemixCount = 0,
                AllowRemix = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            };

            var post2_2 = new Post
            {
                Poster = user2,
                PosterId = user2.Id,
                Design = design2_2,
                DesignId = design2_2.Id,
                Description = "Finding beauty in simplicity ⚪",

                RemixCount = 1,
                AllowRemix = true,
                CreatedAt = DateTime.UtcNow.AddDays(-12)
            };

            var post2_3 = new Post
            {
                Poster = user2,
                PosterId = user2.Id,
                Design = design2_3,
                DesignId = design2_3.Id,
                Description = "Circle study - perfect for subtle chest placement 🎯",

                RemixCount = 0,
                AllowRemix = false,
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            };

            // User 3 Posts
            var post3_1 = new Post
            {
                Poster = user3,
                PosterId = user3.Id,
                Design = design3_1,
                DesignId = design3_1.Id,
                Description = "Back to the 80s! This sunset wave on a jacket is pure nostalgia 🌴✨",

                RemixCount = 0,
                AllowRemix = true,
                CreatedAt = DateTime.UtcNow.AddDays(-8)
            };

            var post3_2 = new Post
            {
                Poster = user3,
                PosterId = user3.Id,
                Design = design3_2,
                DesignId = design3_2.Id,
                Description = "Classic badge design for those who appreciate the vintage aesthetic 🎩",

                RemixCount = 0,
                AllowRemix = true,
                CreatedAt = DateTime.UtcNow.AddDays(-14)
            };

            var post3_3 = new Post
            {
                Poster = user3,
                PosterId = user3.Id,
                Design = design3_3,
                DesignId = design3_3.Id,
                Description = "Remember cassette tapes? Now on your favorite sweatshirt 📼",

                RemixCount = 0,
                AllowRemix = true,
                CreatedAt = DateTime.UtcNow.AddDays(-4)
            };

            // ==================== SOCIAL INTERACTIONS ====================

            // Likes - User 2 likes User 1's posts
            var like1 = new Like
            {
                User = user2,
                UserId = user2.Id,
                Post = post1_1,
                PostId = post1_1.Id,
                LikedAt = DateTime.UtcNow.AddDays(-4),
                CreatedAt = DateTime.UtcNow.AddDays(-4)
            };

            var like2 = new Like
            {
                User = user2,
                UserId = user2.Id,
                Post = post1_2,
                PostId = post1_2.Id,
                LikedAt = DateTime.UtcNow.AddDays(-9),
                CreatedAt = DateTime.UtcNow.AddDays(-9)
            };

            // User 3 likes User 1's and User 2's posts
            var like3 = new Like
            {
                User = user3,
                UserId = user3.Id,
                Post = post1_1,
                PostId = post1_1.Id,
                LikedAt = DateTime.UtcNow.AddDays(-4),
                CreatedAt = DateTime.UtcNow.AddDays(-4)
            };

            var like4 = new Like
            {
                User = user3,
                UserId = user3.Id,
                Post = post2_1,
                PostId = post2_1.Id,
                LikedAt = DateTime.UtcNow.AddDays(-6),
                CreatedAt = DateTime.UtcNow.AddDays(-6)
            };

            var like5 = new Like
            {
                User = user3,
                UserId = user3.Id,
                Post = post2_2,
                PostId = post2_2.Id,
                LikedAt = DateTime.UtcNow.AddDays(-11),
                CreatedAt = DateTime.UtcNow.AddDays(-11)
            };

            // User 1 likes User 3's post
            var like6 = new Like
            {
                User = user1,
                UserId = user1.Id,
                Post = post3_1,
                PostId = post3_1.Id,
                LikedAt = DateTime.UtcNow.AddDays(-7),
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            };

            // Comments
            var comment1 = new Comment
            {
                User = user2,
                UserId = user2.Id,
                Post = post1_1,
                PostId = post1_1.Id,
                Text = "This is absolutely stunning! The neon colors are perfect 🔥",
                CreatedAt = DateTime.UtcNow.AddDays(-4)
            };

            var comment2 = new Comment
            {
                User = user3,
                UserId = user3.Id,
                Post = post1_1,
                PostId = post1_1.Id,
                Text = "Love the cyberpunk vibes! 🐯",
                CreatedAt = DateTime.UtcNow.AddDays(-4)
            };

            var comment3 = new Comment
            {
                User = user1,
                UserId = user1.Id,
                Post = post2_1,
                PostId = post2_1.Id,
                Text = "So clean and simple. Love your minimalist approach!",
                CreatedAt = DateTime.UtcNow.AddDays(-6)
            };

            var comment4 = new Comment
            {
                User = user3,
                UserId = user3.Id,
                Post = post2_1,
                PostId = post2_1.Id,
                Text = "This would be perfect for summer 🏔️",
                CreatedAt = DateTime.UtcNow.AddDays(-6)
            };

            var comment5 = new Comment
            {
                User = user2,
                UserId = user2.Id,
                Post = post3_1,
                PostId = post3_1.Id,
                Text = "Taking me back! Love the retro aesthetic 🌴",
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            };

            // Bookmarks
            var bookmark1 = new Bookmark
            {
                User = user2,
                UserId = user2.Id,
                Post = post1_1,
                PostId = post1_1.Id,
                BookmarkedAt = DateTime.UtcNow.AddDays(-4),
                CreatedAt = DateTime.UtcNow.AddDays(-4)
            };

            var bookmark2 = new Bookmark
            {
                User = user3,
                UserId = user3.Id,
                Post = post1_2,
                PostId = post1_2.Id,
                BookmarkedAt = DateTime.UtcNow.AddDays(-9),
                CreatedAt = DateTime.UtcNow.AddDays(-9)
            };

            var bookmark3 = new Bookmark
            {
                User = user1,
                UserId = user1.Id,
                Post = post2_1,
                PostId = post2_1.Id,
                BookmarkedAt = DateTime.UtcNow.AddDays(-6),
                CreatedAt = DateTime.UtcNow.AddDays(-6)
            };

            var bookmark4 = new Bookmark
            {
                User = user1,
                UserId = user1.Id,
                Post = post3_1,
                PostId = post3_1.Id,
                BookmarkedAt = DateTime.UtcNow.AddDays(-7),
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            };

            // Followers - User 2 and 3 follow User 1, User 1 and 3 follow User 2, User 1 follows User 3
            var follower1 = new Follower
            {
                FollowerUser = user2,
                FollowerId = user2.Id,
                FollowingUser = user1,
                FollowingId = user1.Id,
                IsMutual = true,
                FollowedAt = DateTime.UtcNow.AddDays(-15),
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            };

            var follower2 = new Follower
            {
                FollowerUser = user3,
                FollowerId = user3.Id,
                FollowingUser = user1,
                FollowingId = user1.Id,
                IsMutual = true,
                FollowedAt = DateTime.UtcNow.AddDays(-10),
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            };

            var follower3 = new Follower
            {
                FollowerUser = user1,
                FollowerId = user1.Id,
                FollowingUser = user2,
                FollowingId = user2.Id,
                IsMutual = true,
                FollowedAt = DateTime.UtcNow.AddDays(-14),
                CreatedAt = DateTime.UtcNow.AddDays(-14)
            };

            var follower4 = new Follower
            {
                FollowerUser = user3,
                FollowerId = user3.Id,
                FollowingUser = user2,
                FollowingId = user2.Id,
                IsMutual = false,
                FollowedAt = DateTime.UtcNow.AddDays(-8),
                CreatedAt = DateTime.UtcNow.AddDays(-8)
            };

            var follower5 = new Follower
            {
                FollowerUser = user1,
                FollowerId = user1.Id,
                FollowingUser = user3,
                FollowingId = user3.Id,
                IsMutual = true,
                FollowedAt = DateTime.UtcNow.AddDays(-9),
                CreatedAt = DateTime.UtcNow.AddDays(-9)
            };

            // Notifications
            var notification1 = new Notification
            {
                Recipient = user1,
                RecipientId = user1.Id,
                Sender = user2,
                SenderId = user2.Id,
                Type = NotificationType.Like,
                Post = post1_1,
                PostId = post1_1.Id,
                Message = "MinimalMaven liked your post",
                IsRead = true,
                ActionTaken = false,
                NotifiedAt = DateTime.UtcNow.AddDays(-4),
                CreatedAt = DateTime.UtcNow.AddDays(-4)
            };

            var notification2 = new Notification
            {
                Recipient = user1,
                RecipientId = user1.Id,
                Sender = user3,
                SenderId = user3.Id,
                Type = NotificationType.Comment,
                Post = post1_1,
                PostId = post1_1.Id,
                Message = "VintageVibe commented on your post",
                IsRead = true,
                ActionTaken = false,
                NotifiedAt = DateTime.UtcNow.AddDays(-4),
                CreatedAt = DateTime.UtcNow.AddDays(-4)
            };

            var notification3 = new Notification
            {
                Recipient = user1,
                RecipientId = user1.Id,
                Sender = user2,
                SenderId = user2.Id,
                Type = NotificationType.Follow,
                Message = "MinimalMaven started following you",
                IsRead = true,
                ActionTaken = true,
                NotifiedAt = DateTime.UtcNow.AddDays(-15),
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            };

            var notification4 = new Notification
            {
                Recipient = user2,
                RecipientId = user2.Id,
                Sender = user1,
                SenderId = user1.Id,
                Type = NotificationType.Comment,
                Post = post2_1,
                PostId = post2_1.Id,
                Message = "CyberArtist commented on your post",
                IsRead = false,
                ActionTaken = false,
                NotifiedAt = DateTime.UtcNow.AddDays(-6),
                CreatedAt = DateTime.UtcNow.AddDays(-6)
            };

            var notification5 = new Notification
            {
                Recipient = user2,
                RecipientId = user2.Id,
                Sender = user3,
                SenderId = user3.Id,
                Type = NotificationType.Follow,
                Message = "VintageVibe started following you",
                IsRead = false,
                ActionTaken = false,
                NotifiedAt = DateTime.UtcNow.AddDays(-8),
                CreatedAt = DateTime.UtcNow.AddDays(-8)
            };

            var notification6 = new Notification
            {
                Recipient = user3,
                RecipientId = user3.Id,
                Sender = user2,
                SenderId = user2.Id,
                Type = NotificationType.Bookmark,
                Post = post3_1,
                PostId = post3_1.Id,
                Message = "MinimalMaven bookmarked your post",
                IsRead = false,
                ActionTaken = false,
                NotifiedAt = DateTime.UtcNow.AddDays(-7),
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            };

            // ==================== SETUP USER NAVIGATION PROPERTIES ====================

            // User 1 Navigation Properties
            user1.Designs = new List<Design> { design1_1, design1_2, design1_3 };
            user1.Posts = new List<Post> { post1_1, post1_2, post1_3 };
            user1.LikesGiven = new List<Like> { like6 };
            user1.Comments = new List<Comment> { comment3 };
            user1.Bookmarks = new List<Bookmark> { bookmark3, bookmark4 };
            user1.Followers = new List<Follower> { follower3, follower5 }; // User1 following others
            user1.Notifications = new List<Notification> { notification1, notification2, notification3 };

            // User 2 Navigation Properties
            user2.Designs = new List<Design> { design2_1, design2_2, design2_3 };
            user2.Posts = new List<Post> { post2_1, post2_2, post2_3 };
            user2.LikesGiven = new List<Like> { like1, like2 };
            user2.Comments = new List<Comment> { comment1, comment5 };
            user2.Bookmarks = new List<Bookmark> { bookmark1 };
            user2.Followers = new List<Follower> { follower1, follower4 }; // User2 following others
            user2.Notifications = new List<Notification> { notification4, notification5 };

            // User 3 Navigation Properties
            user3.Designs = new List<Design> { design3_1, design3_2, design3_3 };
            user3.Posts = new List<Post> { post3_1, post3_2, post3_3 };
            user3.LikesGiven = new List<Like> { like3, like4, like5 };
            user3.Comments = new List<Comment> { comment2, comment4 };
            user3.Bookmarks = new List<Bookmark> { bookmark2 };
            user3.Followers = new List<Follower> { follower2 }; // User3 following others
            user3.Notifications = new List<Notification> { notification6 };

            // ==================== SETUP POST NAVIGATION PROPERTIES ====================

            // Post 1_1 - Neon Tiger Circuit
            post1_1.Likes = new List<Like> { like1, like3 };
            post1_1.Comments = new List<Comment> { comment1, comment2 };
            post1_1.Bookmarks = new List<Bookmark> { bookmark1 };

            // Post 1_2 - Digital Samurai
            post1_2.Likes = new List<Like> { like2 };
            post1_2.Comments = new List<Comment>();
            post1_2.Bookmarks = new List<Bookmark> { bookmark2 };

            // Post 1_3 - Glitch Matrix
            post1_3.Likes = new List<Like>();
            post1_3.Comments = new List<Comment>();
            post1_3.Bookmarks = new List<Bookmark>();

            // Post 2_1 - Mountain Line
            post2_1.Likes = new List<Like> { like4 };
            post2_1.Comments = new List<Comment> { comment3, comment4 };
            post2_1.Bookmarks = new List<Bookmark> { bookmark3 };

            // Post 2_2 - Geometric Balance
            post2_2.Likes = new List<Like> { like5 };
            post2_2.Comments = new List<Comment>();
            post2_2.Bookmarks = new List<Bookmark>();

            // Post 2_3 - Circle Study
            post2_3.Likes = new List<Like>();
            post2_3.Comments = new List<Comment>();
            post2_3.Bookmarks = new List<Bookmark>();

            // Post 3_1 - Retro Sunset Wave
            post3_1.Likes = new List<Like> { like6 };
            post3_1.Comments = new List<Comment> { comment5 };
            post3_1.Bookmarks = new List<Bookmark> { bookmark4 };

            // Post 3_2 - Classic Badge
            post3_2.Likes = new List<Like>();
            post3_2.Comments = new List<Comment>();
            post3_2.Bookmarks = new List<Bookmark>();

            // Post 3_3 - Old School Vibes
            post3_3.Likes = new List<Like>();
            post3_3.Comments = new List<Comment>();
            post3_3.Bookmarks = new List<Bookmark>();

            // ==================== ADD ALL DATA TO COLLECTIONS ====================

            Users = new List<User> { user1, user2, user3 };

            Designs = new List<Design>
            {
                design1_1, design1_2, design1_3,
                design2_1, design2_2, design2_3,
                design3_1, design3_2, design3_3
            };

            Posts = new List<Post>
            {
                post1_1, post1_2, post1_3,
                post2_1, post2_2, post2_3,
                post3_1, post3_2, post3_3
            };

            Likes = new List<Like> { like1, like2, like3, like4, like5, like6 };

            Comments = new List<Comment> { comment1, comment2, comment3, comment4, comment5 };

            Bookmarks = new List<Bookmark> { bookmark1, bookmark2, bookmark3, bookmark4 };

            Followers = new List<Follower> { follower1, follower2, follower3, follower4, follower5 };

            Notifications = new List<Notification>
            {
                notification1, notification2, notification3,
                notification4, notification5, notification6
            };
        }
    }
}
