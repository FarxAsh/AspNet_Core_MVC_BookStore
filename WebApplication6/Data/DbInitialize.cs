using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace WebApplication6.Models
{
    public class DbInitializer
    {
        public static async Task InitializeDb(BookStoreContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "Comrade228@";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            if (!context.Author.Any())
            {
                context.Author.AddRange(

                    new Author
                    {
                        FirstName = "Leo",
                        LastName = "Tolstoy",
                        Image = "/Images/Tolstoy.jpg",
                        ShortBiography = "Born to an aristocratic Russian family in 1828,[3] he is best known for the novels " +
                                   "War and Peace (1869) and Anna Karenina (1877),[8] often cited as pinnacles of realist " +
                                   "fiction.He first achieved literary acclaim in his twenties with his semi-autobiographical trilogy, " +
                                   "Childhood, Boyhood, and Youth (1852–1856), and Sevastopol Sketches (1855)",
                        Biography = "Leo Tolstoy, Tolstoy also spelled Tolstoi, Russian in full Lev Nikolayevich, Graf (count) Tolstoy, (born August 28 [September 9, New Style], 1828, Yasnaya Polyana, Tula province, Russian Empire—died November 7 [November 20], 1910, Astapovo, Ryazan province), Russian author, a master of realistic fiction and one of the world’s greatest novelists.Tolstoy is best known for his two longest works, War and Peace (1865–69) and Anna Karenina (1875–77), which are commonly regarded as among the finest novels ever written. War and Peace in particular seems virtually to define this form for many readers and critics. Among Tolstoy’s shorter works, The Death of Ivan Ilyich (1886) is usually classed among the best examples of the novella. Especially during his last three decades Tolstoy also achieved world renown as a moral and religious teacher. His doctrine of nonresistance to evil had an important influence on Gandhi. Although Tolstoy’s religious ideas no longer command the respect they once did, interest in his life and personality has, if anything, increased over the years. Most readers will agree with the assessment of the 19th-century British poet and critic Matthew Arnold that a novel by Tolstoy is not a work of art but a piece of life; the Russian author Isaak Babel commented that, if the world could write by itself, it would write like Tolstoy. Critics of diverse schools have agreed that somehow Tolstoy’s works seem to elude all artifice. Most have stressed his ability to observe the smallest changes of consciousness and to record the slightest movements of the body. What another novelist would describe as a single act of consciousness, Tolstoy convincingly breaks down into a series of infinitesimally small steps. According to the English writer Virginia Woolf, who took for granted that Tolstoy was “the greatest of all novelists,” these observational powers elicited a kind of fear in readers, who “wish to escape from the gaze which Tolstoy fixes on us.” Those who visited Tolstoy as an old man also reported feelings of great discomfort when he appeared to understand their unspoken thoughts. It was commonplace to describe him as godlike in his powers and titanic in his struggles to escape the limitations of the human condition. Some viewed Tolstoy as the embodiment of nature and pure vitality, others saw him as the incarnation of the world’s conscience, but for almost all who knew him or read his works, he was not just one of the greatest writers who ever lived but a living symbol of the search for life’s meaning."

                    },
                      new Author
                      {
                          FirstName = "George",
                          LastName = "Orwell",
                          Image = "/Images/Orwell.jpg",
                          ShortBiography = "Eric Arthur Blair (25 June 1903 – 21 January 1950),better known by his pen name George Orwell, " +
                                      "was an English novelist and essayist, journalist and critic.[2] His work is characterised" +
                                      " by lucid prose, biting social criticism, opposition to totalitarianism, and outspoken support " +
                                      "of democratic socialism",
                          Biography = "George Orwell was an English novelist, essayist and critic most famous for his novels 'Animal Farm' (1945) and 'Nineteen Eighty-Four' (1949). Who Was George Orwell? George Orwell was a novelist, essayist and critic best known for his novels Animal Farm and Nineteen Eighty-Four. He was a man of strong opinions who addressed some of the major political movements of his times, including imperialism, fascism and communism. Family & Early Life Orwell was born Eric Arthur Blair in Motihari, India, on June 25, 1903. The son of a British civil servant, Orwell spent his first days in India, where his father was stationed. His mother brought him and his older sister, Marjorie, to England about a year after his birth and settled in Henley-on-Thames. His father stayed behind in India and rarely visited. (His younger sister, Avril, was born in 1908. Orwell didn't really know his father until he retired from the service in 1912. And even after that, the pair never formed a strong bond. He found his father to be dull and conservative. According to one biography, Orwell's first word was  He was a sick child, often battling bronchitis and the flu. Orwell took up writing at an early age, reportedly composing his first poem around age four. He later wrote, habit of making up stories and holding conversations with imaginary persons, and I think from the very start my literary ambitions were mixed up with the feeling of being isolated and undervalued. One of his first literary successes came at the age of 11 when he had a poem published in the local newspaper."
                      },
                      new Author
                      {
                          FirstName = "Fyodor",
                          LastName = "Dostoevsky",
                          Image = "/Images/Dost.jpg",
                          ShortBiography = "Fyodor Mikhailovich Dostoevsky was a Russian novelist, short story writer, essayist and journalist. " +
                                           "Dostoevsky's literary works explore human psychology in the troubled political, social, and spiritual " +
                                           "atmospheres of 19th-century Russia, and engage with a variety of philosophical and religious themes.",
                          Biography = "Fyodor Mikhailovich Dostoevsky, sometimes transliterated Dostoyevsky, was a Russian novelist, short story writer, essayist and journalist. Dostoevsky's literary works explore human psychology in the troubled political, social, and spiritual atmospheres of 19th-century Russia, and engage with a variety of philosophical and religious themes. His most acclaimed works include Crime and Punishment (1866), The Idiot (1869), Demons (1872), and The Brothers Karamazov (1880). Dostoevsky's body of works consists of 11 novels, three novellas, 17 short stories, and numerous other works. Many literary critics rate him as one of the greatest psychological novelists in world literature.[3] His 1864 novella Notes from Underground is considered to be one of the first works of existentialist literature. Born in Moscow in 1821, Dostoevsky was introduced to literature at an early age through fairy tales and legends, and through books by Russian and foreign authors. His mother died in 1837 when he was 15, and around the same tim,he left school to enter the Nikolayev Military Engineering Institute. After graduating, he worked as an engineer and briefly enjoyed a lavish lifestyle, translating books to earn extra money. In the mid-1840s he wrote his first novel, Poor Folk, which gained him entry into St. Petersburg's literary circles. Arrested in 1849 for belonging to a literary group that discussed banned books critical of Tsarist Russia, he was sentenced to death but the sentence was commuted at the last moment. He spent four years in a Siberian prison camp, followed by six years of compulsory military service in exile. In the following years, Dostoevsky worked as a journalist, publishing and editing several magazines of his own and later A Writer's Diary, a collection of his writings. He began to travel around western Europe and developed a gambling addiction, which led to financial hardship. For a time, he had to beg for money, but he eventually became one of the most widely read and highly regarded Russian writers. Dostoevsky was nfluenced by a wide variety of philosophers and authors including Pushkin, Gogol, Augustine, Shakespeare, Dickens, Balzac, Lermontov, Hugo, Poe, Plato, Cervantes, Herzen, Kant, Belinsky, Hegel, Schiller, Solovyov, Bakunin, Sand, Hoffmann, and Mickiewicz. His writings were widely read both within and beyond his native Russia and influenced an equally great number of later writers including Russians like Aleksandr Solzhenitsyn and Anton Chekhov as well as philosophers such as Friedrich Nietzsche and Jean-Paul Sartre. His books have been translated into more than 170 languages."
                      }

                    );
                context.SaveChanges();
            }
            if (!context.Book.Any())
            {
                context.Book.AddRange(

                    new Book
                    {
                        Title = "War and Peace",
                        Year = 2002,
                        Pages = 800,
                        Description = "War and Peace is a novel by the Russian author Leo Tolstoy, " +
                                      "published serially, then in its entirety in 1869. It is regarded as one of Tolstoy's " +
                                      "finest literary achievements",
                        Image = "/Images/WarAndPeace.jpeg",
                        Genre = Genre.Fiction,
                        Price = 1200,
                        AuthorID = 1
                    },

                        new Book
                        {
                            Title = "1984",
                            Year = 2010,
                            Pages = 200,
                            Description = "Nineteen Eighty-Four: A Novel, often published as 1984, is a dystopian novel by English novelist " +
                                          "George Orwell. It was published on 8 June 1949 by Secker & Warburg as Orwell's ninth and " +
                                          "final book completed in his lifetime.",
                            Genre = Genre.Fantasy,
                            Price = 400,
                            AuthorID = 2,
                            Image = "/Images/Oruel.jpeg",
                        },
                         new Book
                         {
                             Title = "Crime and Punishment",
                             Year = 2010,
                             Pages = 400,
                             Description = "Nineteen Eighty-Four: A Novel, often published as 1984, is a dystopian novel by English novelist " +
                                          "George Orwell. It was published on 8 June 1949 by Secker & Warburg as Orwell's ninth and " +
                                          "final book completed in his lifetime.",
                             Genre = Genre.Fiction,
                             Price = 400,
                             AuthorID = 3,
                             Image = "/Images/Punishment.jpg",
                         }
                    );
                context.SaveChanges();
            }
        }

    }
}
