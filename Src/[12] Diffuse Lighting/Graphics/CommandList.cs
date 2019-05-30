﻿using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class CommandList
    {
        private SharpDX.Direct3D11.DeviceContext NativeDeviceContext;


        public CommandList(GraphicsDevice device) 
        {
            NativeDeviceContext = device.NativeDeviceContext;
        }



        public void Draw(int vertexCount, int startVertexLocation = 0)
        {
            NativeDeviceContext.Draw(vertexCount, startVertexLocation);
        }


        public void DrawIndexed(int indexCount, int startVertexLocation = 0, int baseVertexLocation = 0)
        {
            NativeDeviceContext.DrawIndexed(indexCount, startVertexLocation, baseVertexLocation);
        }


        public void ClearDepthStencilView(Texture texture, DepthStencilClearFlags flags, int depth = 1, byte stencil = 0)
        {
            NativeDeviceContext.ClearDepthStencilView(texture.NativeDepthStencilView, flags, depth, stencil);
        }

        public void SetRenderTargets(Texture texture)
        {
            //set render target
            NativeDeviceContext.OutputMerger.SetRenderTargets(texture.NativeDepthStencilView, texture.NativeRenderTargetView);
        }



        public void SetPrimitiveType(PrimitiveTopology primitiveType)
        {
            //set primitive type
            NativeDeviceContext.InputAssembler.PrimitiveTopology = primitiveType;
        }



        public void SetVertexBuffer(Buffer buffer)
        {
            //set vertex buffer to input stage
            //create buffer binding
            VertexBufferBinding bufferBinding = new VertexBufferBinding(buffer.Resource as SharpDX.Direct3D11.Buffer, buffer.ElementSize, 0);

            //set vertex buffer
            NativeDeviceContext.InputAssembler.SetVertexBuffers(0, bufferBinding);
        }



        public void SetIndexBuffer(Buffer buffer)
        {
            //set index buffer to input stage
            //set index buffer
            NativeDeviceContext.InputAssembler.SetIndexBuffer(buffer.Resource as SharpDX.Direct3D11.Buffer, SharpDX.DXGI.Format.R32_UInt, 0);
        }


        public void SetSampler(ShaderType Type, SamplerState samplerState, int slot)
        {

            switch (Type)
            {
                case ShaderType.None:
                    break;
                case ShaderType.VertexShader:
                    NativeDeviceContext.VertexShader.SetSampler(slot, samplerState.Sampler);
                    break;

                case ShaderType.PixelShader:
                    NativeDeviceContext.PixelShader.SetSampler(slot, samplerState.Sampler);
                    break;

                case ShaderType.HullShader:
                    NativeDeviceContext.HullShader.SetSampler(slot, samplerState.Sampler);
                    break;

                case ShaderType.GeometryShader:
                    NativeDeviceContext.GeometryShader.SetSampler(slot, samplerState.Sampler);
                    break;

                case ShaderType.DomainShader:
                    NativeDeviceContext.DomainShader.SetSampler(slot, samplerState.Sampler);
                    break;

                case ShaderType.ComputeShader:
                    NativeDeviceContext.ComputeShader.SetSampler(slot, samplerState.Sampler);
                    break;
            }

        }

        public void SetShaderResource(ShaderType Type, ShaderResourceView resource, int slot)
        {

            

            switch (Type)
            {
                case ShaderType.None:
                    break;
                case ShaderType.VertexShader:
                    NativeDeviceContext.VertexShader.SetShaderResource(slot, resource);
                    break;

                case ShaderType.PixelShader:
                    NativeDeviceContext.PixelShader.SetShaderResource(slot, resource);
                    break;

                case ShaderType.HullShader:
                    NativeDeviceContext.HullShader.SetShaderResource(slot, resource);
                    break;

                case ShaderType.GeometryShader:
                    NativeDeviceContext.GeometryShader.SetShaderResource(slot, resource);
                    break;

                case ShaderType.DomainShader:
                    NativeDeviceContext.DomainShader.SetShaderResource(slot, resource);
                    break;

                case ShaderType.ComputeShader:
                    NativeDeviceContext.ComputeShader.SetShaderResource(slot, resource);
                    break;
            }
        }


        public void SetInputLayout(InputLayout inputLayout)
        {
            //set input layout Direct3D instance to pipeline
            NativeDeviceContext.InputAssembler.InputLayout = inputLayout;
        }

        public void SetViewPort(float Width, float Height, float X, float Y, float MinDepth = 0.0f, float MaxDepth = 1.0f)
        {

            var viewPortF = new SharpDX.Mathematics.Interop.RawViewportF()
            {
                Width = Width,
                Height = Height,

                X = X,
                Y = Y,

                MinDepth = MinDepth,
                MaxDepth = MaxDepth
            };
            //set view port
            NativeDeviceContext.Rasterizer.SetViewport(viewPortF);
        }

        public void SetVertexShader(VertexShader vertexShader)
        {
            //set vertex shader Direct3D instance to pipeline
            NativeDeviceContext.VertexShader.SetShader(vertexShader, null, 0);
        }

        public void SetPixelShader(PixelShader pixelShader)
        {
            //set pixel shader Direct3D instance to pipeline
            NativeDeviceContext.PixelShader.SetShader(pixelShader, null, 0);
        }


        public  void Clear(Texture renderTarget, Color4 color)
        {
            if (renderTarget == null) throw new ArgumentNullException("RenderTarget");

            NativeDeviceContext.ClearRenderTargetView(renderTarget.NativeRenderTargetView, color);
        }


        public void SetRasterizerState(PipelineState pipelineState)
        {
            //set rasterizerState
            //note: if you change the rasterizer state, you need to reset the state
            NativeDeviceContext.Rasterizer.State = pipelineState.RasterizerState;
        }
    }
}
