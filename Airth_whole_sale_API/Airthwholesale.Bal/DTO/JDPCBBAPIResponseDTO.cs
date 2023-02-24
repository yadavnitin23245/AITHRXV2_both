namespace Airthwholesale.Bal.DTO
{
    public class JDPCBBAPIResponseDTO
    {

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

        public int warning_count { get; set; }
        public int error_count { get; set; }
        public List<object> message_list { get; set; }
        public int token_expiration_minutes { get; set; }
        public UsedVehicles used_vehicles { get; set; }


        public string MessagePath { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AddDeductList
    {
        public string uoc { get; set; }
        public string name { get; set; }
        public int xclean { get; set; }
        public int clean { get; set; }
        public int avg { get; set; }
        public int rough { get; set; }
        public string auto { get; set; }
        public int resid12 { get; set; }
        public int resid24 { get; set; }
        public int resid30 { get; set; }
        public int resid36 { get; set; }
        public int resid42 { get; set; }
        public int resid48 { get; set; }
        public int resid60 { get; set; }
        public int resid72 { get; set; }
    }

    public class KilometerAdjustments
    {
        public int xclean_km_threshold { get; set; }
        public int clean_km_threshold { get; set; }
        public int avg_km_threshold { get; set; }
        public int rough_km_threshold { get; set; }
        public double cents_per_kilometer { get; set; }
    }

  

    public class UsedVehicleList
    {
        public string publish_date { get; set; }
        public string data_freq { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string vin { get; set; }
        public string uvc { get; set; }
        public string groupnum { get; set; }
        public string model_year { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string series { get; set; }
        public string style { get; set; }
        public string mileage_cat { get; set; }
        public string class_code { get; set; }
        public string class_name { get; set; }
        public string description_score { get; set; }
        public bool first_values_flag { get; set; }
        public string risk_score { get; set; }
        public int base_whole_xclean { get; set; }
        public int mileage_whole_xclean { get; set; }
        public int add_deduct_whole_xclean { get; set; }
        public int regional_whole_xclean { get; set; }
        public int adjusted_whole_xclean { get; set; }
        public int base_whole_clean { get; set; }
        public int mileage_whole_clean { get; set; }
        public int add_deduct_whole_clean { get; set; }
        public int regional_whole_clean { get; set; }
        public int adjusted_whole_clean { get; set; }
        public int base_whole_avg { get; set; }
        public int mileage_whole_avg { get; set; }
        public int add_deduct_whole_avg { get; set; }
        public int regional_whole_avg { get; set; }
        public int adjusted_whole_avg { get; set; }
        public int base_whole_rough { get; set; }
        public int mileage_whole_rough { get; set; }
        public int add_deduct_whole_rough { get; set; }
        public int regional_whole_rough { get; set; }
        public int adjusted_whole_rough { get; set; }
        public int base_retail_xclean { get; set; }
        public int mileage_retail_xclean { get; set; }
        public int add_deduct_retail_xclean { get; set; }
        public int regional_retail_xclean { get; set; }
        public int adjusted_retail_xclean { get; set; }
        public int base_retail_clean { get; set; }
        public int mileage_retail_clean { get; set; }
        public int add_deduct_retail_clean { get; set; }
        public int regional_retail_clean { get; set; }
        public int adjusted_retail_clean { get; set; }
        public int base_retail_avg { get; set; }
        public int mileage_retail_avg { get; set; }
        public int add_deduct_retail_avg { get; set; }
        public int regional_retail_avg { get; set; }
        public int adjusted_retail_avg { get; set; }
        public int base_retail_rough { get; set; }
        public int mileage_retail_rough { get; set; }
        public int add_deduct_retail_rough { get; set; }
        public int regional_retail_rough { get; set; }
        public int adjusted_retail_rough { get; set; }
        public int msrp { get; set; }
        public int retail_equipped { get; set; }
        public string price_includes { get; set; }
        public decimal wheel_base { get; set; }
        public string tire_size { get; set; }
        public decimal gvw { get; set; }
        public string payload_cap { get; set; }
        public string seat_cap { get; set; }
        public string fuel_type { get; set; }
        public string fuel_cap { get; set; }
        public string fuel_delivery { get; set; }
        public string hwy_mpg { get; set; }
        public string city_mpg { get; set; }
        public string engine_description { get; set; }
        public string cylinders { get; set; }
        public string engine_displacement { get; set; }
        public string base_hp { get; set; }
        public string torque { get; set; }
        public string transmission { get; set; }
        public string drivetrain { get; set; }
        public string num_gears { get; set; }
        public string ext_doors { get; set; }
        public string airbags { get; set; }
        public string anti_corrosion_warranty { get; set; }
        public string basic_warranty { get; set; }
        public string powertrain_warranty { get; set; }
        public string road_assist_warranty { get; set; }
        public List<AddDeductList> add_deduct_list { get; set; }
        public KilometerAdjustments kilometer_adjustments { get; set; }
        public List<string> model_number_list { get; set; }
        public string moon_sunroof { get; set; }
        public string navigation { get; set; }
        public string seats { get; set; }
    }

    public class UsedVehicles
    {
        public List<UsedVehicleList> used_vehicle_list { get; set; }
        public int template { get; set; }
        public int version { get; set; }
        public bool data_included { get; set; }
        public bool data_available { get; set; }
    }



}

