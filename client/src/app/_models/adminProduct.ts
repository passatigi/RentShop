export interface AdminProduct {
  id?: number
  name?: string
  vendor?: string
  description?: string
  categoryId?: number
  categoryName?: string
  productImgsLinks?: string[]
  productFeatures?: ProductFeature[]
  realProducts?: RealProduct[]
}

export interface ProductFeature {
  featureId?: number
  name?: string
  explanation?: any
  groupName?: string
  value?: string
}

export interface RealProduct {
  id: number
  serialNumber: string
  condition: string
  rentPrice: number
}