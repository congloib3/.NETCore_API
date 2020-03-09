khoi tao controller
    public void ConfigureServices(IServiceCollection services)
            {
                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            }

            public void Configure(IApplicationBuilder app, IHostingEnvironment env)
            {
                app.UseMvc();
            }

khoi tao Models(EF ContextDB)
    - command.cs(khai bao sql)

    -> intsall entityframework with command(dotnet ef)

    - commandContext(anh xa csdl sang sqlserver)
        using Microsoft.EntityFrameworkCore;

            namespace CmdApi.Models
            {
                public class CommandContext : DbContext
                {
                    public CommandContext(DbContextOptions<CommandContext> options) :base(options)
                    {

                    }
                    public DbSet<Command> CommandItems {get;set;}
                    
                }
            }

    -> tao chuoi ket noi(appsettings.json)
                {
                    "Data":
                    {
                        "CommandAPIConnection":
                        {
                        "ConnectionString":"Server=DESKTOP-D8P9S63\\MSSQLSERVER01;Database=CmdApi;Trusted_Connection=True"
                        }
                    }
                }
    -> cau hinh co so du lieu, Đăng ký file database context với file Startup.cs(startup.cs)
        public IConfiguration Configuration{get;}
        public Startup(IConfiguration configuration)=>Configuration = configuration;

        //add DbContext voi sqlserver

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CommandContext> //Trong Asp.net core, các services như DB context phải được đăng ký với Dependency Injection Container, khi đó cái container mới cung cấp các service này cho các Controllers được.
                (opt=>opt.UseSqlServer(Configuration["Data:CommandAPIConnection:ConnectionString"]));
        }
    -> tao file migrations voi lenh (dotnet ef migrations add <Name of migrations>)
    -> update first new data (dotnet ef database update)

    -> using file Models de co the tuong tac duoc voi co so du lieu
        private readonly CommandContext _context;

        public CommandsController(CommandContext context) => _context = context;
        Constructor của CommandsController có khai báo CommandContext, cái context này được inject từ container. Ta dùng đối tượng context này để thực hiện CRUD

