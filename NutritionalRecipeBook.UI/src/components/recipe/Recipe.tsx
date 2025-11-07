import { useState } from "react";

import { PlusOutlined, MinusCircleOutlined} from "@ant-design/icons";
import { useCreateRecipeMutation } from '@api';
import type { IngredientModel, RecipeModel } from "@models";
import { toast } from '@utils/toast.tsx';

import { Button, Checkbox, FloatButton, Form, Input, InputNumber, Modal, Space, Spin, } from 'antd';
import { Content } from "antd/es/layout/layout";

function Recipe() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [form] = Form.useForm<RecipeModel>();
  const [createRecipe, { isLoading, isError }] = useCreateRecipeMutation();

  const handleOpen = () => setIsModalOpen(true);
  const handleCancel = () => setIsModalOpen(false);
  
  const handleSubmit = async (values: any) => {
    const payload = {
      recipeDTO: {
        name: values.name.trim(),
        description: values.description?.trim(),
        instructions: values.instructions?.trim(),
        cookingTimeInMin: values.cookingTimeInMin,
        servings: values.servings
      },
      ingredients: values.ingredients?.map((ing: IngredientModel) => ({
        ingredientDTO: {
          name: ing.name.trim(),
          isLiquid: ing.isLiquid ?? false
        },
        amount: ing.amount,
        unit: ing.unit.trim()
      })) ?? []
    };

    console.log("Recipe Payload Sent to Backend:", payload);

    try {
      await createRecipe(payload).unwrap();
      toast("Recipe created successfully!");
      form.resetFields();
      setIsModalOpen(false);
    } catch (error) {
      console.error("Failed to create recipe:", error);
      toast("Failed to create recipe");
    }
  };

  return (
    <Content className="p-6 relative min-h-screen flex">
      <FloatButton
        icon={<PlusOutlined />}
        type="primary"
        tooltip={<div>Add Recipe</div>}
        onClick={handleOpen}
      />

      <Modal
        title="Create New Recipe"
        open={isModalOpen}
        onCancel={handleCancel}
        destroyOnClose
        footer={null}
        className="max-h-[70vh] overflow-y-auto"
      >
        <Spin spinning={isLoading} tip="Creating recipe...">
          <Form form={form} layout="vertical" onFinish={handleSubmit}>
            <Form.Item
              name="name"
              label="Recipe Name"
              rules={[{ required: true, message: "Please enter the recipe name" }]}
            >
              <Input placeholder="e.g. Spaghetti Carbonara" />
            </Form.Item>

            <Form.Item
              name="description"
              label="Description"
              rules={[{ required: true, message: "Please enter a description" }]}
            >
              <Input.TextArea rows={2} placeholder="Brief description" />
            </Form.Item>

            <Form.List name="ingredients">
              {(fields, { add, remove }) => (
                <>
                  <label className="font-medium mb-2 block">Ingredients</label>
                  {fields.map(({ key, name, ...restField }) => (
                    <Space
                      key={key}
                      style={{
                        display: "flex",
                        marginBottom: 8,
                        alignItems: "center",
                      }}
                      align="baseline"
                    >
                      <Form.Item
                        {...restField}
                        name={[name, "name"]}
                        rules={[{ required: true, message: "Enter ingredient name" }]}
                      >
                        <Input placeholder="Ingredient name" />
                      </Form.Item>

                      <Form.Item
                        {...restField}
                        name={[name, "amount"]}
                        rules={[{ required: true, message: "Enter amount" }]}
                      >
                        <InputNumber min={0.1} placeholder="Amount" />
                      </Form.Item>

                      <Form.Item
                        {...restField}
                        name={[name, "unit"]}
                        rules={[{ required: true, message: "Enter unit" }]}
                      >
                        <Input placeholder="e.g. g, ml" />
                      </Form.Item>

                      <Form.Item
                        {...restField}
                        name={[name, "isLiquid"]}
                        valuePropName="checked"
                        style={{ marginBottom: 0 }}
                      >
                        <Checkbox>Liquid</Checkbox>
                      </Form.Item>

                      <MinusCircleOutlined
                        onClick={() => remove(name)}
                        style={{ color: "red", marginLeft: 8 }}
                      />
                    </Space>
                  ))}
                  <Form.Item>
                    <Button
                      type="dashed"
                      onClick={() => add()}
                      block
                      icon={<PlusOutlined />}
                    >
                      Add Ingredient
                    </Button>
                  </Form.Item>
                </>
              )}
            </Form.List>

            <Form.Item
              name="instructions"
              label="Instructions"
              rules={[{ required: true, message: "Please enter instructions" }]}
            >
              <Input.TextArea rows={3} placeholder="Step-by-step instructions..." />
            </Form.Item>

            <Form.Item
              name="cookingTimeInMin"
              label="Cooking Time (minutes)"
              rules={[{ required: true, message: "Please enter cooking time" }]}
            >
              <InputNumber min={1} style={{ width: "100%" }} />
            </Form.Item>

            <Form.Item
              name="servings"
              label="Servings"
              rules={[{ required: true, message: "Please enter number of servings" }]}
            >
              <InputNumber min={1} style={{ width: "100%" }} />
            </Form.Item>

            <Form.Item>
              <Button type="primary" htmlType="submit" block loading={isLoading}>
                {isLoading ? "Creating..." : "Create Recipe"}
              </Button>
            </Form.Item>
          </Form>
        </Spin>
      </Modal>

      {isError && toast("Failed to create recipe")}
    </Content>
  );
}

export default Recipe;
