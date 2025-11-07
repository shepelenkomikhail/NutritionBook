import { useState } from "react";
import { Content } from "antd/es/layout/layout";
import { FloatButton, Modal, Form, Input, InputNumber, Button, Spin} from "antd";
import { PlusOutlined } from "@ant-design/icons";

import type { RecipeModel } from "@models";
import { useCreateRecipeMutation } from '@api';
import { toast } from '@utils/toast.tsx';

function Recipe() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [form] = Form.useForm<RecipeModel>();

  const handleOpen = () => setIsModalOpen(true);
  const handleCancel = () => setIsModalOpen(false);

  const [createRecipe, { isLoading, isError }] = useCreateRecipeMutation();

  const handleSubmit = async (newRecipe: RecipeModel) => {
    console.log("Recipe created:", newRecipe);

    try {
      await createRecipe(newRecipe).unwrap();
      toast("Recipe created successfully");
    } catch (error) {
      toast("Failed to create recipe");
      console.error("Failed to create recipe:", error);
    }

    form.resetFields();
    setIsModalOpen(false);
  };


  return (
    <Content className={"p-6 relative min-h-screen flex"}>
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
        className={"max-h-[70vh] overflow-y-auto"}
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

            <Form.Item
              name="ingredients"
              label="Ingredients"
              rules={[{ required: true, message: "Please enter ingredients" }]}
            >
              <Input.TextArea rows={3} placeholder="List ingredients..." />
            </Form.Item>

            <Form.Item
              name="instructions"
              label="Instructions"
              rules={[
                { required: true, message: "Please enter cooking instructions" },
              ]}
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
              <Button
                type="primary"
                htmlType="submit"
                block
                loading={isLoading}
              >
                {isLoading ? "Creating..." : "Create Recipe"}
              </Button>
            </Form.Item>
          </Form>
        </Spin>
      </Modal>

      {isError && (toast("Failed to crate a recipe")) }
    </Content>
  );
}

export default Recipe;
