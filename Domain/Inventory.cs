using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace InventorySystem.Domain
{
    public class Inventory : INotifyPropertyChanged
    {
        private List<Part> _parts = new List<Part>();
        private List<Product> _products = new List<Product>();

        // Searches Parts List by it's search term and returns that single term and displays it
        public IEnumerable<Part> Parts => _parts.Where(p => p.Name.Contains(PartSearchTerm ?? "", StringComparison.CurrentCultureIgnoreCase));
        // Searches Products List by it's search term and returns that single term and displays it
        public IEnumerable<Product> Products => _products.Where(p => p.Name.Contains(ProductSearchTerm ?? "", StringComparison.CurrentCultureIgnoreCase));

        public string PartSearchTerm { get; set; }
        public string ProductSearchTerm { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Seeds Inventory with example Data
        public Inventory(bool seedInitialInventory = true)
        {
            if (seedInitialInventory)
            {
                SeedInventory();
            }
        }


        // Product Actions
        public void AddProduct(Product product)
        {
            // Simply adds a product to the products list
            _products.Add(product);
        }

        public bool RemoveProduct(int productId)
        {
            // Filters product by ID, when found, continues to remove the product
            // if not found throws an exception
            var productToRemove = Products.Where(p => p.ProductId == productId).FirstOrDefault();
            if (productToRemove == null)
                throw new Exception($"A product with ID #{productId} could not be found.");

            // Product cannot be deleted if it has parts associated with it.
            if (productToRemove.AssociatedParts.Any())
                throw new Exception("Product has associated parts and cannot be deleted.  Please remove parts first.");

            _products.Remove(productToRemove);
            return true;
        }

        public Product LookupProduct(int productId)
        {
            return _products.Where(p => p.ProductId == productId).FirstOrDefault();
        }

        public void UpdateProduct(int productId, Product product)
        {
            // Finds product by ID, when found continues to update the specific product
            // with the user input, throws an exception if not found.
            var productToUpdate = _products.Where(p => p.ProductId == productId).FirstOrDefault();

            if (productToUpdate == null)
                throw new Exception(message: $"A product with ID #{productId} could not be found.");

            productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;
            productToUpdate.InStock = product.InStock;
            productToUpdate.Min = product.Min;
            productToUpdate.Max = product.Max;
        }


        // Part Actions
        public void AddPart(Part part)
        {
            _parts.Add(part);
        }

        public bool DeletePart(Part part)
        {
            // Delete a part by part ID
            var partToDelete = Parts.Where(p => p.PartId == part.PartId).FirstOrDefault();
            if (partToDelete == null) return false;

            _parts.Remove(part);
            return true;
        }

        public Part LookupPart(int partId)
        {
            return _parts.Where(p => p.PartId == partId).FirstOrDefault();
        }

        public void UpdatePart(int partId, Part part)
        {
            // Searches the specific part to update by ID
            var partToUpdate = _parts.Where(p => p.PartId == partId).FirstOrDefault();

            if (partToUpdate == null)
                throw new Exception(message: $"A part with ID #{partId} could not be found.");

            var index = _parts.IndexOf(partToUpdate);

            if (index != -1)
            {
                _parts[index] = part;
            }
        }

        // Example Data for Inventory Parts and Associated parts
        private void SeedInventory()
        {
            _parts.Add(new InhousePart
            {
                PartId = 1,
                Name = "Door",
                Price = 5.01m,
                InStock = 9,
                Min = 1,
                Max = 15,
                MachineId = 1
            });

            _parts.Add(new OutsourcedPart
            {
                PartId = 2,
                Name = "Window",
                Price = 8.11m,
                InStock = 17,
                Min = 1,
                Max = 25,
                CompanyName = "Power Corp"
            });

            _parts.Add(new OutsourcedPart
            {
                PartId = 3,
                Name = "Wheel",
                Price = 7.02m,
                InStock = 10,
                Min = 1,
                Max = 20,
                CompanyName = "Ford"
            });

            // Seeded Associated Part
            var associatedParts = new ObservableCollection<Part>();
            associatedParts.Add(LookupPart(1));

            _products.Add(new Product
            {
                ProductId = 1,
                Name = "Handle",
                Price = 17.13m,
                InStock = 600,
                Min = 100,
                Max = 999,
                AssociatedParts = associatedParts
            });
        }
    }
}
