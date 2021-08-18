export interface NewProduct {
    name?: string
    vendor?: string
    description?: string
    categoryId?: number
    productFeatures?: NewProductFeature[]
}

export interface NewProductFeature {
    featureId?: number
    value?: string
}