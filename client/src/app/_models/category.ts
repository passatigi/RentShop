export interface Category {
    id: number
    childCategories: Category[]
    parentCategoryId?: number
    name?: string
    imgLink?: string
}