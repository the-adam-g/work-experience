# How to add a new endpoint

## Step 1. Add a class
The first step into creating the new endpoint is to add a class for the API data. This allows for each API JSON object to be put into an object which the code later accesses.
**How to add to the code**
```
using System.Text.Json.Serialization;

namespace HarryPotter.Server.Models
{
    public class [CLASS NAME]
    {
        [JsonPropertyName("JSON CATEGORY HERE")]
        public [DATA TYPE HERE]? [VARIABLE NAME HERE] { get; set; }
        [JsonPropertyName("JSON CATEGORY HERE")]
        public [DATA TYPE HERE]? [VARIABLE NAME HERE] { get; set; }
        ...
    }
}
```
**Example of integration with books**
```
using System.Text.Json.Serialization;

namespace HarryPotter.Server.Models
{
    public class Book
    {
        [JsonPropertyName("number")]
        public int? ID { get; set; }
        [JsonPropertyName("title")]
        public string? Name { get; set; }
        [JsonPropertyName("releaseDate")]
        public string? releaseDate { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("cover")]
        public string? Cover { get; set; }
    }
}
```

## Step 2. Alter HarryPotterService
Next, add a get asynchronous method to get the API data. **Asynchronous means that the API data can be fetched without freezing the page.**

**How to add to the code**
```
public async Task<List<[LIST NAME]>> Get[ITEM NAME]Async()
{
    HttpResponseMessage response = await _httpClient.GetAsync($"[API URL HERE]");
    response.EnsureSuccessStatusCode();

    string json = await response.Content.ReadAsStringAsync();
    List<[LIST NAME]>? [variable names] = JsonSerializer.Deserialize<List<Book>>(json);

    return [variable names] ?? new List<[LIST NAME]>();
}
```

**Example of integration with books**
```
public async Task<List<Book>> GetBooksAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://potterapi-fedeperin.vercel.app/en/books"); //defines url for the api request + page
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            List<Book>? books = JsonSerializer.Deserialize<List<Book>>(json);

            return books ?? new List<Book>();
        }
```

## Step 3. Add razor page
Razor pages act as the frontend using C# and HTML to write a page

**Example of integration with books**
```
@page "/[page name]" 
@using Blazorise.LoadingIndicator
@using HarryPotter.Client.Core.Models @* Import classes *@
@using HarryPotter.Client.Core.Services
@using HarryPotter.Server.Models @* import more classes *@
@inject HarryPotterService HarryPotterService
@* Defines a new URL directory for the file *@

<div class="search-field">
    <Field Horizontal>
        <FieldLabel ColumnSize="ColumnSize.Is1">Name:</FieldLabel>
        <FieldBody ColumnSize="ColumnSize.Is4">
            <input class="form-control" placeholder="Enter book title here" @bind="SearchText" @bind:event="oninput" />
        </FieldBody>
    </Field>
</div>

<table class="table table-striped table-hover spells-table">
    <thead>
        <tr>
            <th>Name</th>
            <th>Description</th>
            <th>Release Date</th>
        </tr>
    </thead>
    <tbody>
        @foreach ([Class name] item in [FilteredList])
        {
            <tr>
                <td>@[variable].Name</td>
                <td>@[variable].Description</td>
                <td>@[variable].releaseDate</td>
            </tr>
        }
    </tbody>
</table>

@{
    <style scoped>
        .search-field {
            padding: 10px;
        }

        .spells-table {
            margin-top: 10px;
        }
    </style>
}

@code {
    private string SearchText = string.Empty;

    private bool IsLoading;

    private List<Item> ItemList { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        BookList = await HarryPotterService.GetBooksAsync(); //Set spell link to API call
        IsLoading = false;
    }
    private IEnumerable<Book> [FilteredList]
    {
        get
        {
            if (string.IsNullOrWhiteSpace(BookSearchText)) //if there is no data, ignore character
            {
                return BookList;
            }
            return BookList.Where(character =>
                character.Name?.Contains(BookSearchText, StringComparison.OrdinalIgnoreCase) ?? false);
        }
    }
}
```
**Example of a razor page code for Books. This is adaptable to any**
```
@page "/books" 
@using Blazorise.LoadingIndicator
@using HarryPotter.Client.Core.Models @* Import classes *@
@using HarryPotter.Client.Core.Services
@using HarryPotter.Server.Models @* import more classes *@
@inject HarryPotterService HarryPotterService
@* Defines a new URL directory for the file *@

<div class="search-field">
    <Field Horizontal>
        <FieldLabel ColumnSize="ColumnSize.Is1">Name:</FieldLabel>
        <FieldBody ColumnSize="ColumnSize.Is4">
            <input class="form-control" placeholder="Enter book title here" @bind="BookSearchText" @bind:event="oninput" />
        </FieldBody>
    </Field>
</div>

<table class="table table-striped table-hover spells-table">
    <thead>
        <tr>
            <th>Name</th>
            <th>Description</th>
            <th>Release Date</th>
        </tr>
    </thead>
    <tbody>
        @foreach (Book book in FilteredBooks)
        {
            <tr>
                <td>@book.Name</td>
                <td>@book.Description</td>
                <td>@book.releaseDate</td>
            </tr>
        }
    </tbody>
</table>

@{
    <style scoped>
        .search-field {
            padding: 10px;
        }

        .spells-table {
            margin-top: 10px;
        }
    </style>
}

@code {
    private string BookSearchText = string.Empty;

    private bool IsLoading;

    private List<Book> BookList { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        BookList = await HarryPotterService.GetBooksAsync(); //Set spell link to API call
        IsLoading = false;
    }
    private IEnumerable<Book> FilteredBooks
    {
        get
        {
            if (string.IsNullOrWhiteSpace(BookSearchText)) //if there is no data, ignore character
            {
                return BookList;
            }
            return BookList.Where(character =>
                character.Name?.Contains(BookSearchText, StringComparison.OrdinalIgnoreCase) ?? false);
        }
    }
}
```