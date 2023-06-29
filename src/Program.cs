string current = Directory.GetCurrentDirectory();
string name = Path.GetFileName(current);

Console.WriteLine($"Renaming project files to '{name}'. . .");

ProcessFiles(current);
ProcessFiles(Path.Combine(current, "src"));

void ProcessFiles(string dir)
{
    foreach (var file in Directory.EnumerateFiles(dir, "*.*")) {
        string newFile = Path.Combine(dir, Path.GetRelativePath(dir, file).Replace("NxEditor.PluginTemplate", name));

        string value = File.ReadAllText(file);
        value = value.Replace("NxEditor.PluginTemplate", name);

        File.Delete(file);
        File.WriteAllText(newFile, value);
    }
}