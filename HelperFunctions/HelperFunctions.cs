using InvoiceDemo.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceDemo.HelperFunctions
{
    public class HelperFunctions
    {
        static public List<ValidationError> ValidateRow(ExcelWorksheet sheet, int RowNumber)
        {
            var ErrorList = new List<ValidationError>();
            var BranchId = sheet.Cells[RowNumber, 1].Value?.ToString();
            if (!TypeHelpers.IsInteger(BranchId))
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 1,
                    Message = "Branch ID Must Be Integer!"
                });
            ErrorList.AddRange(CheckMandatoryCellsEmptyOrNull(ErrorList, sheet, RowNumber));
            ErrorList.AddRange(CheckStringCells(ErrorList, sheet, RowNumber));
            ErrorList.AddRange(CheckFloatInputs(ErrorList, sheet, RowNumber));

            return ErrorList;
        }
        static public List<ValidationError> CheckStringCells(List<ValidationError> ErrorList,
            ExcelWorksheet sheet, int RowNumber)
        {
            // string cells
            var Country = sheet.Cells[RowNumber, 2].Value?.ToString();
            var Governate = sheet.Cells[RowNumber, 3].Value?.ToString();
            var City = sheet.Cells[RowNumber, 4].Value?.ToString();
            var Street = sheet.Cells[RowNumber, 5].Value?.ToString();
            var Floor = sheet.Cells[RowNumber, 8].Value?.ToString();
            var Room = sheet.Cells[RowNumber, 9].Value?.ToString();
            var Landmark = sheet.Cells[RowNumber, 10].Value?.ToString();
            var AdditonalInfo = sheet.Cells[RowNumber, 11].Value?.ToString();
            var CustomerType = sheet.Cells[RowNumber, 12].Value?.ToString();
            var CustomerId = sheet.Cells[RowNumber, 13].Value?.ToString();
            var CustomerName = sheet.Cells[RowNumber, 14].Value?.ToString();
            var InvoiceDocumentType = sheet.Cells[RowNumber, 15].Value?.ToString();
            var InvoiceTaxPayeCoder = sheet.Cells[RowNumber, 17].Value?.ToString();
            var InvoiceInternalId = sheet.Cells[RowNumber, 18].Value?.ToString();
            var ItemDescription = sheet.Cells[RowNumber, 19].Value?.ToString();
            var ItemType = sheet.Cells[RowNumber, 20].Value?.ToString();
            var ItemCode = sheet.Cells[RowNumber, 21].Value?.ToString();
            var ItemUnit = sheet.Cells[RowNumber, 22].Value?.ToString();
            var ItemInternalId = sheet.Cells[RowNumber, 24].Value?.ToString();
            var ItemCurrencySold = sheet.Cells[RowNumber, 28].Value?.ToString();
            var ItemTaxType = sheet.Cells[RowNumber, 34].Value?.ToString();
            var ItemSubType = sheet.Cells[RowNumber, 36].Value?.ToString();

            if (Country is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 2,
                    Message = "Country must be string"
                });
            }
            if (Governate is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 3,
                    Message = "Governate must be string"
                });
            }
            if (City is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 4,
                    Message = "City must be string"
                });
            }
            if (Street is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 5,
                    Message = "Street must be string"
                });
            }
            if (Floor is not string && !TypeHelpers.IsNullOrEmpty(Floor))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 8,
                    Message = "Floor must be string"
                });
            }
            if (Room is not string && !TypeHelpers.IsNullOrEmpty(Room))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 9,
                    Message = "Room must be string"
                });
            }
            if (Landmark is not string && !TypeHelpers.IsNullOrEmpty(Landmark))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 10,
                    Message = "Landmark must be string"
                });
            }
            if (AdditonalInfo is not string && !TypeHelpers.IsNullOrEmpty(AdditonalInfo))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 11,
                    Message = "AdditonalInfo must be string"
                });
            }
            if (CustomerType is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 12,
                    Message = "CustomerType must be string"
                });
            }
            if (CustomerId is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 13,
                    Message = "CustomerId must be string"
                });
            }
            if (CustomerName is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 14,
                    Message = "CustomerName must be string"
                });
            }
            if (InvoiceDocumentType is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 15,
                    Message = "Invoice Document Type must be string"
                });
            }
            if (InvoiceTaxPayeCoder is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 17,
                    Message = "Invoice Tax Paye Coder must be string"
                });
            }
            if (InvoiceInternalId is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 18,
                    Message = "Invoice Internal Id must be string"
                });
            }
            if (ItemDescription is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 19,
                    Message = "Item Description must be string"
                });
            }
            if (ItemType is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 20,
                    Message = "Item Type must be string"
                });
            }
            if (ItemCode is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 21,
                    Message = "Item Code must be string"
                });
            }
            if (ItemUnit is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 22,
                    Message = "Item Unit must be string"
                });
            }
            if (ItemInternalId is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 24,
                    Message = "Item Internal Id must be string"
                });
            }
            if (ItemCurrencySold is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 28,
                    Message = "Item Currency Sold must be string"
                });
            }
            if (ItemTaxType is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 34,
                    Message = "Item Tax Type must be string"
                });
            }
            if (ItemSubType is not string)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 36,
                    Message = "Item Sub Type must be string"
                });
            }

            return ErrorList;
        }
        static public List<ValidationError> CheckMandatoryCellsEmptyOrNull(List<ValidationError> ErrorList,
            ExcelWorksheet sheet, int RowNumber)
        {
            var BranchId = sheet.Cells[RowNumber, 1].Value?.ToString();
            var Country = sheet.Cells[RowNumber, 2].Value?.ToString();
            var Governate = sheet.Cells[RowNumber, 3].Value?.ToString();
            var City = sheet.Cells[RowNumber, 4].Value?.ToString();
            var Street = sheet.Cells[RowNumber, 5].Value?.ToString();
            var BuildingNo = sheet.Cells[RowNumber, 6].Value?.ToString();
            var CustomerType = sheet.Cells[RowNumber, 12].Value?.ToString();
            var CustomerId = sheet.Cells[RowNumber, 13].Value?.ToString();
            var CustomerName = sheet.Cells[RowNumber, 14].Value?.ToString();
            var InvoiceDocumentType = sheet.Cells[RowNumber, 15].Value?.ToString();
            var InvoiceDate = sheet.Cells[RowNumber, 16].Value?.ToString();
            var InvoiceTaxPayeCoder = sheet.Cells[RowNumber, 17].Value?.ToString();
            var InvoiceInternalId = sheet.Cells[RowNumber, 18].Value?.ToString();
            var ItemDescription = sheet.Cells[RowNumber, 19].Value?.ToString();
            var ItemType = sheet.Cells[RowNumber, 20].Value?.ToString();
            var ItemCode = sheet.Cells[RowNumber, 21].Value?.ToString();
            var ItemUnit = sheet.Cells[RowNumber, 22].Value?.ToString();
            var ItemQty = sheet.Cells[RowNumber, 23].Value?.ToString();
            var ItemInternalId = sheet.Cells[RowNumber, 24].Value?.ToString();
            var ItemSalesTotal = sheet.Cells[RowNumber, 25].Value?.ToString();
            var ItemGrandTotal = sheet.Cells[RowNumber, 26].Value?.ToString();
            var ItemNetTotal = sheet.Cells[RowNumber, 27].Value?.ToString();
            var ItemCurrencySold = sheet.Cells[RowNumber, 28].Value?.ToString();
            var ItemAmountPrice = sheet.Cells[RowNumber, 29].Value?.ToString();
            var ItemDiscountRate = sheet.Cells[RowNumber, 32].Value?.ToString();
            var ItemDiscountAmount = sheet.Cells[RowNumber, 33].Value?.ToString();
            var ItemTaxType = sheet.Cells[RowNumber, 34].Value?.ToString();
            var ItemTaxAmount = sheet.Cells[RowNumber, 35].Value?.ToString();
            var ItemSubType = sheet.Cells[RowNumber, 36].Value?.ToString();
            var ItemTaxRate = sheet.Cells[RowNumber, 37].Value?.ToString();
            var InvoiceSalesTotal = sheet.Cells[RowNumber, 38].Value?.ToString();
            var InvoiceNetTotal = sheet.Cells[RowNumber, 39].Value?.ToString();
            var InvoiceGrandTotal = sheet.Cells[RowNumber, 40].Value?.ToString();
            var InvoiceDiscount = sheet.Cells[RowNumber, 41].Value?.ToString();

            if (TypeHelpers.IsNullOrEmpty(BranchId))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 1,
                    Message = "Branch Id cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(Country))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 2,
                    Message = "Country cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(Governate))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 3,
                    Message = "Governate cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(City))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 4,
                    Message = "City cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(Street))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 5,
                    Message = "Street cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(BuildingNo))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 6,
                    Message = "Building Number cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(CustomerType))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 12,
                    Message = "Customer Type cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(CustomerId))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 13,
                    Message = "Customer Id cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(CustomerName))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 14,
                    Message = "Customer Name cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(InvoiceDocumentType))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 15,
                    Message = "Invoice Document Type cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(InvoiceDate))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 16,
                    Message = "Invoice Date cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(InvoiceTaxPayeCoder))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 17,
                    Message = "Invoice TaxPaye Code cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(InvoiceInternalId))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 18,
                    Message = "Invoice Internal Id cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemDescription))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 19,
                    Message = "Item Description cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemType))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 20,
                    Message = "Item Type cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemCode))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 21,
                    Message = "Item Code cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemUnit))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 22,
                    Message = "Item Unit cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemQty))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 23,
                    Message = "Item Qty cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemInternalId))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 24,
                    Message = "Item Internal Id cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemSalesTotal))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 25,
                    Message = "Item Sales Total cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemGrandTotal))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 26,
                    Message = "Item Grand Total cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemNetTotal))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 27,
                    Message = "Item Net Total cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemCurrencySold))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 28,
                    Message = "Item Currency Sold cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemAmountPrice))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 29,
                    Message = "Item Amount Price cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemDiscountRate))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 32,
                    Message = "Item Discount Rate cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemDiscountAmount))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 33,
                    Message = "Item Discount Amount cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemTaxType))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 34,
                    Message = "Item Tax Type cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemTaxAmount))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 35,
                    Message = "Item Tax Amount cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemSubType))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 36,
                    Message = "Item Sub Type cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(ItemTaxRate))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 37,
                    Message = "Item Tax Rate cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(InvoiceSalesTotal))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 38,
                    Message = "Invoice Sales Total cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(InvoiceNetTotal))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 39,
                    Message = "Invoice Net Total cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(InvoiceGrandTotal))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 40,
                    Message = "Invoice Grand Total cannot be null or empty!"
                });
            }
            if (TypeHelpers.IsNullOrEmpty(InvoiceDiscount))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 41,
                    Message = "Invoice Discount cannot be null or empty!"
                });
            }

            return ErrorList;
        }
        static public List<ValidationError> CheckFloatInputs(List<ValidationError> ErrorList,
            ExcelWorksheet sheet, int RowNumber)
        {
            var ItemQty = sheet.Cells[RowNumber, 23].Value?.ToString();
            var ItemSalesTotal = sheet.Cells[RowNumber, 25].Value?.ToString();
            var ItemGrandTotal = sheet.Cells[RowNumber, 26].Value?.ToString();
            var ItemNetTotal = sheet.Cells[RowNumber, 27].Value?.ToString();
            var ItemAmountPrice = sheet.Cells[RowNumber, 29].Value?.ToString();
            var ItemDiscountRate = sheet.Cells[RowNumber, 32].Value?.ToString();
            var ItemDiscountAmount = sheet.Cells[RowNumber, 33].Value?.ToString();
            var ItemTaxAmount = sheet.Cells[RowNumber, 35].Value?.ToString();
            var ItemTaxRate = sheet.Cells[RowNumber, 37].Value?.ToString();
            var InvoiceSalesTotal = sheet.Cells[RowNumber, 38].Value?.ToString();
            var InvoiceNetTotal = sheet.Cells[RowNumber, 39].Value?.ToString();
            var InvoiceGrandTotal = sheet.Cells[RowNumber, 40].Value?.ToString();
            var InvoiceDiscount = sheet.Cells[RowNumber, 41].Value?.ToString();


            if (!TypeHelpers.IsFloat(ItemQty))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 23,
                    Message = "Item Qty should have numbers only"
                });
            }
            if (!TypeHelpers.IsFloat(ItemSalesTotal))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 25,
                    Message = "Item Sales Total should have numbers only"
                });
            }
            if (!TypeHelpers.IsFloat(ItemGrandTotal))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 26,
                    Message = "Item Grand Total should have numbers only"
                });
            }
            if (!TypeHelpers.IsFloat(ItemNetTotal))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 27,
                    Message = "Item Net Total should have numbers only"
                });
            }
            if (!TypeHelpers.IsFloat(ItemAmountPrice))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 29,
                    Message = "Item Amount Price should have numbers only"
                });
            }
            if (!TypeHelpers.IsFloat(ItemDiscountRate))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 32,
                    Message = "Item Discount Rate should have numbers only"
                });
            }
            if (!TypeHelpers.IsFloat(ItemDiscountAmount))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 33,
                    Message = "Item Discount Amount should have numbers only"
                });
            }
            if (!TypeHelpers.IsFloat(ItemTaxAmount))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 35,
                    Message = "Item Tax Amount should have numbers only"
                });
            }
            if (!TypeHelpers.IsFloat(ItemTaxRate))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 37,
                    Message = "Item Tax Rate should have numbers only"
                });
            }
            if (!TypeHelpers.IsFloat(InvoiceSalesTotal))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 38,
                    Message = "Invoice Sales Total should have numbers only"
                });
            }
            if (!TypeHelpers.IsFloat(InvoiceNetTotal))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 39,
                    Message = "Invoice Net Total should have numbers only"
                });
            }
            if (!TypeHelpers.IsFloat(InvoiceGrandTotal))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 40,
                    Message = "Invoice Grand Total should have numbers only"
                });
            }
            if (!TypeHelpers.IsFloat(InvoiceDiscount))
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 41,
                    Message = "Invoice Discount should have numbers only"
                });
            }

            return ErrorList;
        }
        static public List<ValidationError> CheckItemTotals(List<ValidationError> ErrorList,
            ExcelWorksheet sheet, int RowNumber)
        {
            // item cells
            var ItemQty = decimal.Parse(sheet.Cells[RowNumber, 23].Value?.ToString());
            var ItemSalesTotal = decimal.Parse(sheet.Cells[RowNumber, 25].Value?.ToString());
            var ItemGrandTotal = decimal.Parse(sheet.Cells[RowNumber, 26].Value?.ToString());
            var ItemNetTotal = decimal.Parse(sheet.Cells[RowNumber, 27].Value?.ToString());
            var ItemAmountPrice = decimal.Parse(sheet.Cells[RowNumber, 29].Value?.ToString());
            var ItemAmountSold = decimal.Parse(sheet.Cells[RowNumber, 30].Value?.ToString());
            var ItemcurrencyExchange = decimal.Parse(sheet.Cells[RowNumber, 31].Value?.ToString());
            var ItemDiscountRate = decimal.Parse(sheet.Cells[RowNumber, 32].Value?.ToString());
            var ItemDiscountAmount = decimal.Parse(sheet.Cells[RowNumber, 33].Value?.ToString());
            var ItemTaxAmount = decimal.Parse(sheet.Cells[RowNumber, 35].Value?.ToString());
            var ItemTaxRate = decimal.Parse(sheet.Cells[RowNumber, 37].Value?.ToString());
            var ItemCurrencySold = sheet.Cells[RowNumber, 28].Value?.ToString();

            if (ItemCurrencySold is not "EGP")
            {
                ItemAmountPrice = ItemAmountSold * ItemcurrencyExchange;
                var AmountEGP = decimal.Parse(sheet.Cells[RowNumber, 29].Value?.ToString());
                if(AmountEGP != ItemAmountPrice)
                {
                    ErrorList.Add(new ValidationError
                    {
                        Row = RowNumber,
                        Col = 29,
                        Message = $"Amount EGP calculation failed"
                    });
                }
            }

            var salesTotal = ItemQty * ItemAmountPrice;
            var discAmount = (ItemDiscountRate / 100) * salesTotal;
            var netTotal = salesTotal - discAmount;
            var taxAmount = (ItemTaxRate / 100) * netTotal;
            var grandTotal = netTotal + taxAmount;

            if (ItemSalesTotal != salesTotal)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 25,
                    Message = $"re-calculate item sales total"
                });
            }
            if (ItemDiscountAmount != discAmount)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 33,
                    Message = "Please re-calculate Item discount amount!"
                });
            }
            if (ItemNetTotal != netTotal)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 27,
                    Message = $"re-calculate item Net total"
                });
            }
            if (ItemTaxAmount != taxAmount)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 35,
                    Message = "Please re-calculate Item tax amount!"
                });
            }
            if (ItemGrandTotal != grandTotal)
            {
                ErrorList.Add(new ValidationError
                {
                    Row = RowNumber,
                    Col = 26,
                    Message = $"re-calculate item grand total"
                });
            }

            return ErrorList;
        }

        static public List<decimal> GetTotalPerRow(ExcelWorksheet sheet, int RowNumber,
            decimal Sales, decimal Net, decimal Grand)
        {
            // item cells
            var ItemQty = decimal.Parse(sheet.Cells[RowNumber, 23].Value?.ToString());
            var ItemAmountPrice = decimal.Parse(sheet.Cells[RowNumber, 29].Value?.ToString());
            var ItemAmountSold = decimal.Parse(sheet.Cells[RowNumber, 30].Value?.ToString());
            var ItemcurrencyExchange = decimal.Parse(sheet.Cells[RowNumber, 31].Value?.ToString());
            var ItemDiscountRate = decimal.Parse(sheet.Cells[RowNumber, 32].Value?.ToString());

            var ItemTaxRate = decimal.Parse(sheet.Cells[RowNumber, 37].Value?.ToString());
            var ItemCurrencySold = sheet.Cells[RowNumber, 28].Value?.ToString();

            if (ItemCurrencySold is not "EGP")
            {
                ItemAmountPrice = ItemAmountSold * ItemcurrencyExchange;
            }

            var salesTotal = ItemQty * ItemAmountPrice;
            Sales += salesTotal;
            var discAmount = (ItemDiscountRate / 100) * salesTotal;
            var netTotal = salesTotal - discAmount;
            Net += netTotal;
            var taxAmount = (ItemTaxRate / 100) * netTotal;
            var grandTotal = netTotal + taxAmount;
            Grand += grandTotal;

            return new List<decimal> { Sales, Net, Grand };
        }
    }
}
