using SeleniteSeaCore.codeblocks;
using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaExecutor
{
    public static class SerializationEngine
    {
        /// <summary>
        /// Serializes a script
        /// </summary>
        /// <param name="function">Function to save</param>
        /// <param name="file">location</param>
        public static void Serialize(SSBlock ToSerialize, string file)
        {
            using StreamWriter writer = new(file);

            Stack<(SSBlock block, int depth)> ToVisit = [];//keep track of elements and depth
            ToVisit.Push((ToSerialize, 0));

            int previousDepth = 0;
            while(ToVisit.Count > 0)
            {
                var (block, depth) = ToVisit.Pop();
                var data = block.GetSerializedMetadata();

                if (previousDepth > depth)
                    writer.WriteLine("<--");
                else if (previousDepth < depth)
                    writer.WriteLine("-->");

                writer.WriteLine(block.GetType());
                writer.WriteLine(data.Length.ToString());
                foreach( var line in data)
                    writer.WriteLine(line.ToString());

                //Current block can be a parent
                if (typeof(SSBlockScope).IsAssignableFrom(block.GetType()))
                {
                    SSBlockScope scope = (SSBlockScope)block;
                    for (int i = scope.Children.Count - 1; i >= 0; i--)
                        ToVisit.Push((scope.Children[i], depth + 1));
                }
            }

            writer.Close();
        }
        /// <summary>
        /// Reads a file and deserializes a script within
        /// </summary>
        /// <param name="editorRegisteredTypes">Usually passed by the editor or created in the main method of a compiler</param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public static SSBlock? Deserialize(Dictionary<string,Type> RegisteredBlocks, string file)
        {
            SSBlock? root = null;
            SSBlockScope? CurrentParent = null;

            using StreamReader reader = new(file);
            

            //Element Creation
            SSBlock? workpiece = null;
            int metadataLinesLeft = 0;
            List<string> metadata = [];
            string? line;
            while((line = reader.ReadLine())!= null)
            {
                if(metadataLinesLeft == 0 && workpiece != null)
                {
                    //apply metadata
                    workpiece.DeserializeAndApplyMetadata([.. metadata]);

                    //if move arrows, then change parent
                    if(line == "-->")
                    {
                        if (!typeof(SSBlockScope).IsAssignableFrom(workpiece.GetType()))
                            throw new InvalidDataException("Attempted to make a parent of an non child bearing block");
                        CurrentParent = (SSBlockScope)workpiece;
                    }
                    else if(line == "<--")
                    {
                        CurrentParent = CurrentParent?.Parent;
                    }
                    //reset workpiece
                    workpiece = null;
                    metadata.Clear();
                    
                }
                if (metadataLinesLeft > 0)
                {
                    metadata.Add(line);
                    metadataLinesLeft--;
                }
                if (workpiece == null) //New workpiece
                {
                    if (!RegisteredBlocks.TryGetValue(line,out Type? blocktype))
                        throw new InvalidDataException($"Deserialization error: Type {line} not registered");
                    workpiece = Instantiate(blocktype);
                    CurrentParent?.AddChild(workpiece);
                    
                }
                root ??= workpiece;
            }
            reader.Close();
            return root;
        }
        public static SSBlock Instantiate(Type blocktype)
        {
            if (!typeof(SSBlock).IsAssignableFrom(blocktype))
                throw new InvalidOperationException($"{blocktype} doesn't inherit from SSBlock and cannot be a part of the structure.");

            //Try get constructor of that block type from the SSCore
            var ctor = blocktype.GetConstructor([])
                    ?? throw new InvalidOperationException($"{blocktype} couldn't be instantiated. Missing blank constructor.");

            //Invoke the constructor and pass parent variables as params

            var Block = ctor.Invoke([]) as SSBlock
                ?? throw new InvalidOperationException($"{blocktype} couldn't be instantiated. Constructor returned null");
            return Block;
        }
    }
}
