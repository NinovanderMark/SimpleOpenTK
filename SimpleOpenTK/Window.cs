using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace SimpleOpenTK
{
    public class Window : GameWindow
    {
        public const string ASSETS_PATH = "assets";
        public const string WINDOW_TITLE = "Learning OpenTK";

        private readonly ILogger<Window> _logger;

        private int _vertexBufferObject;
        private int _vertexArrayObject;

        private float[] _vertices = {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
             0.5f, -0.5f, 0.0f, //Bottom-right vertex
             0.0f,  0.5f, 0.0f  //Top vertex
        };
        
        private Shader? _baseShader;

        public Window(ILogger<Window> logger, IOptions<VideoConfiguration> video)
            : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (video.Value.ScreenWidth, video.Value.ScreenHeight), Title = WINDOW_TITLE })
        {
            _logger = logger;
            _logger.LogInformation($"Game initialized with screen size {video.Value.ScreenWidth}x{video.Value.ScreenHeight}");
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);


            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            _baseShader = new Shader("shader.vert", "shader.frag", _logger);
            _baseShader.Use();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            return;

            // Unbind all the resources by binding the targets to 0/null.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);

            GL.DeleteProgram(_baseShader.Handle);

            base.OnUnload();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            _baseShader.Use();

            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);

            _logger.LogInformation($"Screen resized to {e.Width}x{e.Height}");
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                _logger.LogInformation("Somebody pressed 'Escape'");
                Close();
            }
        }
    }
}
