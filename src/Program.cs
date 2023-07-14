﻿const string sourceName = "NxEditor.PluginTemplate";

string current = Directory.GetCurrentDirectory();
string name = Path.GetFileName(current);

Console.WriteLine($"Renaming project files to '{name}'. . .");

ProcessFiles(current);
ProcessFolder(current);

void ProcessFolder(string dir)
{
    foreach (var folder in Directory.EnumerateDirectories(dir).Where(x => Path.GetFileName(x) is not ".git" or "lib")) {
        ProcessFiles(folder);
        ProcessFolder(folder);
    }
}

void ProcessFiles(string dir)
{
    foreach (var file in Directory.EnumerateFiles(dir, "*.*")) {
        string newFile = Path.Combine(dir, Path.GetRelativePath(dir, file).Replace(sourceName, name));

        string value = File.ReadAllText(file);
        value = value.Replace(sourceName, name);

        try {
            File.Delete(file);
            File.WriteAllText(newFile, value);
        }
        catch {
            Console.WriteLine($"Skipped '{file}'");
        }
    }
}