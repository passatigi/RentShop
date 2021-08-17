export interface Category {
    id: number
    childCategories: Category[]
    name?: string
    imgLink?: string
}